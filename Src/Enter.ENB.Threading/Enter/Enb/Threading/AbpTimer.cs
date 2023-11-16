using Enter.ENB.DependencyInjection;
using Enter.ENB.ExceptionHandling;
using Enter.ENB.Exceptions;
using Enter.ENB.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Enter.Enb.Threading;

/// <summary>
/// A robust timer implementation that ensures no overlapping occurs. It waits exactly specified <see cref="Period"/> between ticks.
/// </summary>
public class AbpTimer : ITransientDependency
{
    /// <summary>
    /// This event is raised periodically according to Period of Timer.
    /// </summary>
    public event EventHandler Elapsed = default!;

    /// <summary>
    /// Task period of timer (as milliseconds).
    /// </summary>
    public int Period { get; set; }

    /// <summary>
    /// Indicates whether timer raises Elapsed event on Start method of Timer for once.
    /// Default: False.
    /// </summary>
    public bool RunOnStart { get; set; }

    public ILogger<AbpTimer> Logger { get; set; }

    public IExceptionNotifier ExceptionNotifier { get; set; }

    private readonly Timer _taskTimer;
    private volatile bool _performingTasks;
    private volatile bool _isRunning;

    public AbpTimer()
    {
        ExceptionNotifier = NullExceptionNotifier.Instance;
        Logger = NullLogger<AbpTimer>.Instance;

        _taskTimer = new Timer(
            TimerCallBack!,
            null,
            Timeout.Infinite,
            Timeout.Infinite
        );
    }

    public void Start(CancellationToken cancellationToken = default)
    {
        if (Period <= 0)
        {
            throw new EntException("Period should be set before starting the timer!");
        }

        lock (_taskTimer)
        {
            _taskTimer.Change(RunOnStart ? 0 : Period, Timeout.Infinite);
            _isRunning = true;
        }
    }

    public void Stop(CancellationToken cancellationToken = default)
    {
        lock (_taskTimer)
        {
            _taskTimer.Change(Timeout.Infinite, Timeout.Infinite);
            while (_performingTasks)
            {
                Monitor.Wait(_taskTimer);
            }

            _isRunning = false;
        }
    }

    /// <summary>
    /// This method is called by _taskTimer.
    /// </summary>
    /// <param name="state">Not used argument</param>
    private void TimerCallBack(object state)
    {
        lock (_taskTimer)
        {
            if (!_isRunning || _performingTasks)
            {
                return;
            }

            _taskTimer.Change(Timeout.Infinite, Timeout.Infinite);
            _performingTasks = true;
        }

        try
        {
            Elapsed.InvokeSafely(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            Logger.LogException(ex);
            AsyncHelper.RunSync(() => ExceptionNotifier.NotifyAsync(ex));
        }
        finally
        {
            lock (_taskTimer)
            {
                _performingTasks = false;
                if (_isRunning)
                {
                    _taskTimer.Change(Period, Timeout.Infinite);
                }

                Monitor.Pulse(_taskTimer);
            }
        }
    }
}

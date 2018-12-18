namespace Asteroids.Renderer
{
    public class GameClock
    {
        
        private float _timeScale
        /*class Clock
        {
            U64 m_timeCycles;
            F32 m_timeScale;
            bool m_isPaused;
            static F32 s_cyclesPerSecond;
            static inline U64 secondsToCycles(F32 timeSeconds)
            {
                return (U64)(timeSeconds * s_cyclesPerSecond);
            }
            
            // WARNING: Dangerous -- only use to convert small
            // durations into seconds.
            static inline F32 cyclesToSeconds(U64 timeCycles)
            {
                return (F32)timeCycles / s_cyclesPerSecond;
            }
            
            public:
            // Call this when the game first starts up.
            static void init()
            {
                s_cyclesPerSecond
                    = (F32)readHiResTimerFrequency();
            }
            
            // Construct a clock.
            explicit Clock(F32 startTimeSeconds = 0.0f) :
                m_timeCycles( secondsToCycles(startTimeSeconds)),
            m_timeScale( 1.0f), // default to unscaled
            m_isPaused( false) // default to running
            {
            }
            
            // Return the current time in cycles. NOTE that we do
            // not return absolute time measurements in floating
            // point seconds, because a 32-bit float doesn’t have
            // enough precision. See calcDeltaSeconds().
            
            U64 getTimeCycles() const
            {
                return m_timeCycles;
            }
            
            // Determine the difference between this clock’s
            // absolute time and that of another clock, in
            // seconds. We only return time deltas as floating
            // point seconds, due to the precision limitations of
            // a 32-bit float.
            F32 calcDeltaSeconds(const Clock& other)
            {
                U64 dt = m_timeCycles – other.m_timeCycles;
                return cyclesToSeconds(dt);
            }
            // This function should be called once per frame,
            // with the real measured frame time delta in seconds.
            void update(F32 dtRealSeconds)
            {
                if (!m_isPaused)
                {
                    U64 dtScaledCycles
                        = secondsToCycles(
                            dtRealSeconds * m_timeScale);
                    m_timeCycles += dtScaledCycles;
                }
            }
            void setPaused(bool isPaused)
                         {
                             m_isPaused = isPaused;
                         }
            bool isPaused() const
            {
                return m_isPaused;
            }
            void setTimeScale(F32 scale)
            {
                m_timeScale = scale;
            }
            F32 getTimeScale() const
            {
                return m_timeScale;
            }
            void singleStep()
            {
                if (m_isPaused)
                {
                    // Add one ideal frame interval; don’t forget
                    // to scale it by our current time scale!
                    U64 dtScaledCycles = secondsToCycles(
                        ( 1.0f/30.0f) * m_timeScale);
                    m_timeCycles += dtScaledCycles;
                }
            }
        };*/
    }
}
namespace _Global {
    public class Timer {
        public float TimeToWait { get; private set; }
        public float CurrentTime { get; private set; }
        public Timer(float timeToWait) {
            SetTime(timeToWait);
        }
        
        public bool Tick(float deltaTime) {
            //CurrentTime -= deltaTime;
            if (CurrentTime <= 0f) {
                CurrentTime = 0f;
                return true; // Timer has finished
            }
            return false; // Timer is still running
        }
        
        public void SetTime(float time) {
            TimeToWait = time;
            CurrentTime = time;
        }
    }
}
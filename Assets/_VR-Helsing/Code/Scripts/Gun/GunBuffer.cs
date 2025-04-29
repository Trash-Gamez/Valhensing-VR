namespace _VanHelsingVR.Animation.Gun
{
    public class GunBuffer
    {
        public const int MAX_BUFFER_SIZE = 4;
        public readonly float[] Buffer = new float[MAX_BUFFER_SIZE];
        
        private int _bufferIterator = -1;

        public GunBuffer()
        {
            Buffer = new float[MAX_BUFFER_SIZE];
        }

        public void Add(float value)
        {
            if (++_bufferIterator >= MAX_BUFFER_SIZE)
            {
                _bufferIterator = 0;
            }
            
            Buffer[_bufferIterator] = value;
        }

        public float Average()
        {
            var sum = 0.0f;

            for (int i = 0; i < MAX_BUFFER_SIZE; i++)
            {
                sum += Buffer[i];
            }
            
            return sum / MAX_BUFFER_SIZE;
        }
    }
}
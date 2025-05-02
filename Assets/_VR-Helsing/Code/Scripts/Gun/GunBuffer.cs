namespace _VanHelsingVR.Animation.Gun
{
    public class GunBuffer
    {
        public const int MAX_BUFFER_SIZE = 5;
        public readonly float[] Buffer;
        
        private int _bufferIterator = 0;

        public GunBuffer()
        {
            Buffer = new float[MAX_BUFFER_SIZE];
        }

        public void Add(float value)
        {
            Buffer[_bufferIterator] = value;
            
            if (++_bufferIterator >= MAX_BUFFER_SIZE)
            {
                _bufferIterator = 0;
            }
        }
        
        public float Last()
        {
            return Buffer[_bufferIterator];
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

        public void Reset()
        {
            for (int i = 0; i < MAX_BUFFER_SIZE; i++)
            {
                Buffer[i] = 0;
            }
            
            _bufferIterator = 0;
        }
    }
}
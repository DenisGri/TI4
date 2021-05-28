namespace TI4
{
    public class Lfsr
    {
        readonly bool[] _bits;
        public Lfsr(int bitCount, string seed)
        {
            _bits = new bool[bitCount];

            for (var i = 0; i < bitCount; i++)
                _bits[i] = seed[i] == '1' ? true : false;

        }

        private char Shift()
        {
            var bnew = _bits[2] ^ _bits[27] & true;
            var result = _bits[^1] ? '1' : '0';
            for (var i = _bits.Length - 1; i > 0; i--)
            {
                _bits[i] = _bits[i - 1];
            }
            _bits[0] = bnew;
            return result;
        }
        public char[] GetKey(int length)
        {
            var key = new char[length * 8];
            for (var i = 0; i < length * 8; i++)
            {
                key[i] = Shift();
            }
            return key;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace IdeBridge
{
    public class ArgsParser
    {
        Char [] _sep = new Char[] { ',' };
        int _argsIndex = 0;
        string[] _args = null;
        
        public void Init(object input) 
        {
            _args = (input as string).Split(_sep, StringSplitOptions.RemoveEmptyEntries);
            _argsIndex = 0;
        }

        public IntPtr[] ReadIntPtrArray()  
        {
            IntPtr[] result = new IntPtr[_args.Length];
            for(int i =0; i < _args.Length; i++)  
            {
                result[i] = ReadIntPtr(_args[i]);
            }
            return result;
        }

        public IntPtr ReadIntPtr()  
        {
            return ReadIntPtr(_args[_argsIndex++]);
        }

        public IntPtr ReadIntPtr(string value)  
        {
            if( IntPtr.Size == 8 ) 
            {
                return new IntPtr(Int32.Parse(value));
            }
            else
            {
                return new IntPtr(Int64.Parse(value));
            }
        }

        public int ReadInt()  
        {
            return Int32.Parse(_args[_argsIndex++]);
        }
    }
}

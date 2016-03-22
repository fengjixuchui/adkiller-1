using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdKiller
{
    public class CircularBuffer
    {
        private int start;
        private int end;
        private short[] buffer;
        public int Start
        {
            get
            {
                // Unikamy modyfikacji danych w getterze    
                return start;
            }
            private set
            {
                this.start = value;
            }
        }
        public int End
        {
            get
            {
                return end;
            }
            private set
            {
                this.end = value;
            }
        }
        public int Size
        {
            get
            {
                if (End >= Start)
                    return End - Start;
                else
                    return buffer.Length - Start + End;
            }
        }

        public CircularBuffer(int size)
        {
            if (size < 1)
            {
                throw new ArgumentException("Size must be a positive value", "Size");
            }
            else
            {
                buffer = new short[size];
                Start = 0; // points to the first element
                End = 0; // End - 1 is a last element of the buffer. End points to the first empty which can be filled
            }

        }
        public void Push(short[] newItems)
        {
            if (newItems == null)
                throw new ArgumentNullException("Passed null!");

            if (Size + newItems.Length > buffer.Length) // if buffer size is not sufficent then create bigger one
            {
                short[] newBuffer = new short[Size + newItems.Length];

                if (start < end) // if data is not wrapped
                {
                    Array.Copy(buffer, start, newBuffer, 0, Size);
                    Array.Copy(newItems, 0, newBuffer, Size, newItems.Length);
                    end = Size + newItems.Length;
                    start = 0;
                    buffer = newBuffer;
                }
                else // if data is wrapped
                {
                    int distance = buffer.Length - start;
                    Array.Copy(buffer, start, newBuffer, 0, distance);
                    Array.Copy(buffer, 0, newBuffer, distance, end);
                    Array.Copy(newItems, 0, newBuffer, Size, newItems.Length);
                    end = Size + newItems.Length;
                    start = 0;
                    buffer = newBuffer;
                }
            }
            else // if buffer size is sufficent
            {
                if (start > end) // if data is wrapped
                {
                    Array.Copy(newItems, 0, buffer, end, newItems.Length);
                    end += newItems.Length;
                }
                else
                {
                    if (end + newItems.Length <= buffer.Length) // data will fit
                    {
                        Array.Copy(newItems, 0, buffer, end, newItems.Length);
                        end += newItems.Length;
                    }
                    else // wrap data
                    {
                        int distance = buffer.Length - end; // distance till end of the buffer
                        Array.Copy(newItems, 0, buffer, end, distance);
                        Array.Copy(newItems, distance, buffer, 0, newItems.Length - distance);
                        end = newItems.Length - distance;

                    }
                }
            }
        }
        //public void Push(short[] newItems)
        //{
        //    if (newItems == null)
        //        throw new ArgumentNullException("Passed null!");

        //    int newSize = End + newItems.Length;
        //    if (newSize >= buffer.Length) // Warunek nie uwzględnia zawinięcia!
        //    {
        //        if (newSize % buffer.Length >= Start) // Podwójne zawinięcie
        //        {
        //            // Przeniesienie aktualnych danych na początek

        //            // create new larger buffer to extEnd filled, old one 
        //            Array.Resize(ref buffer, newSize);
        //            Array.Copy(newItems, 0, buffer, End, newItems.Length);
        //            End = newSize;

        //        }


        //        else
        //        {
        //            if (Size + newItems.Length > buffer.Length)
        //            {
        //                // create larger buffer and place data in right place ( like the middle of the buffer )
        //                short[] newBuffer = new short[Size];
        //                if (Start > End) // if data wrapped
        //                {

        //                    Array.Copy(buffer, Start, newBuffer, 0, buffer.Length - Start); // copy of wrapped data
        //                    Array.Copy(buffer, 0, newBuffer, buffer.Length - Start, End);

        //                    // Podmieniamy buffer z newBuffer (szybciej)
        //                    Array.Resize(ref buffer, Size + newItems.Length); // resize buffer
        //                    Array.Clear(buffer, 0, buffer.Length);   // clear data
        //                    Array.Copy(newBuffer, 0, buffer, 0, newBuffer.Length); // restore data at the beggining
        //                    Array.Copy(newItems, 0, buffer, newBuffer.Length, newItems.Length); // add new data
        //                    Start = 0;
        //                    End = newBuffer.Length + newItems.Length;
        //                }
        //                else
        //                {
        //                    Array.Copy(buffer, Start, newBuffer, 0, Size); // copy of data
        //                    // Aktualizacja End
        //                    // Bufor nie jest powiększany
        //                }


        //            }
        //            else
        //            {
        //                // Zawinięcie może nie być konieczne
        //                // add  data at the End and wrap it to the beginning
        //                int EndDistance = buffer.Length - End;
        //                Array.Copy(newItems, 0, buffer, Start, EndDistance);
        //                Array.Copy(newItems, EndDistance, buffer, 0, newItems.Length - EndDistance);
        //                End = newSize % buffer.Length;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //  just add data
        //        Array.Copy(newItems, 0, buffer, End, newItems.Length);
        //        End = newSize;

        //    }
        //}
        public short[] Pop(int numOfElements)
        {
            if (numOfElements <= 0 || Size < numOfElements)
                throw new ArgumentOutOfRangeException("Number of elements must be positive! Can't pop up from empty buffer!");
            short[] tempArray = new short[numOfElements];
            int newStart = Start + numOfElements;
            if (Start < End)
            {


                Array.Copy(buffer, Start, tempArray, 0, numOfElements);
#if DEBUG
                Array.Clear(buffer, Start, numOfElements);
#endif
                Start = newStart;


            }
            else
            {

                int distance = buffer.Length - Start;
                if (distance < numOfElements)
                {

                    Array.Copy(buffer, Start, tempArray, 0, distance);
                    Array.Clear(buffer, Start, distance);
                    Array.Copy(buffer, 0, tempArray, distance, numOfElements - distance);
                    Array.Clear(buffer, 0, numOfElements - distance);
                    Start = numOfElements - distance;
                    if (Start == End)
                        Start = End = 0;

                }
                else
                {

                    Array.Copy(buffer, Start, tempArray, 0, numOfElements);
                    Array.Clear(buffer, Start, numOfElements);
                    Start += numOfElements;
                    if (Start == End)
                        Start = End = 0;

                }


            }
            return tempArray;
        }
        public short[] PopAll()
        {
            short[] bufferArray = new short[buffer.Length];
            bufferArray = this.Pop(this.Size);
            return bufferArray;
        }

        public short[] Buffer
        {
            get
            {
                return buffer;
            }
        }
    }

}
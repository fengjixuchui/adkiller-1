using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdKiller.utils.test
{
    [TestClass]
    public class VerifyPatternTest
    {
        [TestMethod]
        public void VerifyWhereStartOK()
        {

            // Arrange
            short[] data = new short[1000],
                    pattern = new short[100];
            // Fill with sample data
            for (int i = 0; i < pattern.Length; i++)
            {
                if (i % 2 == 0)
                    pattern[i] = -2;
                else
                    pattern[i] = 2;
            }
            for (int i = 0; i < data.Length; i++)
            {
                if (i >= 900)
                {
                    if (i % 2 == 0)
                    {
                        data[i] = -2;
                    }
                    else
                    {
                        data[i] = 2;
                    }
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        data[i] =-1;
                    }
                    else
                    {
                        data[i] =1;
                    }
                }
            }
            // Act
            int expected = 900,
                actual = SoundProcessor.FindPattern(data, pattern);
            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void VerifyWhereStartFALSE()
        {
            // Arrange
            short[] data = new short[1000],
                    pattern = new short[100];
            // Fill with data
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (short)i;
            }

            for (int i = 0; i < pattern.Length; i++)
            {
                pattern[i] = (short)(i * i * i);
            }
            // Act
            int expected = -1,
            actual = SoundProcessor.FindPattern(data, pattern);
            // Assert
            Assert.AreEqual(expected, actual);
        }
    }

    [TestClass]
    public class CircularBufferTest
    {
        [TestMethod]
        public void PopUpAllValues()
        {
            // Arrange
            CircularBuffer testBuffer = new CircularBuffer(10);
            // Fill with data
            short[] array1 = { 1, 2, 3, 4 };
            short[] array2 = { 5, 6, 7, 8, 9 };
            // Act
            testBuffer.Push(array1);
            testBuffer.Push(array1);
            var actual = testBuffer.Pop(8);
            short[] expected = { 1, 2, 3, 4, 1, 2, 3, 4 };
            CollectionAssert.AreEqual(expected, actual);
            // Assert
        }
        [TestMethod]
        public void CircularPopUp()
        {
            // Arrange
            CircularBuffer testBuffer = new CircularBuffer(10);
            // Fill with data
            short[] array1 = { 1, 2, 3, 4 };
            short[] array2 = { 5, 6, 7, 8, 9 };
            // Act
            testBuffer.Push(array2);
            testBuffer.Push(array2);
            var data = testBuffer.Pop(4);
            data = testBuffer.Pop(3);
            testBuffer.Push(array1);
            var actual = testBuffer.Buffer;
            short[] expected = { 1, 2, 3, 4, 0, 0, 0, 7, 8, 9 };
            CollectionAssert.AreEqual(expected, actual);
            // Assert
        }
        [TestMethod]
        public void WrappedDataPushResize()
        {
            // Arrange
            CircularBuffer testBuffer = new CircularBuffer(10);
            // Fill data
            short[] array1 = { 1, 2, 3, 4 };
            short[] array2 = { 5, 6, 7, 8, 9 };
            short[] array3 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            // Act
            testBuffer.Push(array2);
            testBuffer.Push(array2);
            var data = testBuffer.Pop(6);
            testBuffer.Push(array1);
            testBuffer.Push(array3);
            var actual = testBuffer.Buffer;
            short[] expected = { 6, 7, 8, 9, 1, 2, 3, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void PushUpResizeBuffer()
        {
            // Arrange
            CircularBuffer testBuffer = new CircularBuffer(10);
            // Fill with data
            short[] array1 = { 1, 2, 3, 4 };
            short[] array2 = { 5, 6, 7, 8, 9 };
            // Act
            testBuffer.Push(array2);
            testBuffer.Push(array1);
            testBuffer.Push(array1);
            var actual = testBuffer.Buffer;
            short[] expected = { 5, 6, 7, 8, 9, 1, 2, 3, 4, 1, 2, 3, 4 };
            CollectionAssert.AreEqual(expected, actual);
            // Assert
        }
        [TestMethod]
        public void BigPushOnWrappedBuffer()
        {
            // Arrange
            CircularBuffer testBuffer = new CircularBuffer(10);
            // Fill with data
            short[] array1 = { 5, 6, 7, 8, 9 };
            short[] array2 = { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 };
            // Act
            testBuffer.Push(array1);
            testBuffer.Push(array1);
            var dara = testBuffer.Pop(5);
            testBuffer.Push(array2);
            var actual = testBuffer.Buffer;
            short[] expected = { 5, 6, 7, 8, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 };
            // Assert
            CollectionAssert.AreEqual(expected, actual);


        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BadArgumentBuffer()
        {
            // Exception
            CircularBuffer testBuffer = new CircularBuffer(-5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArrayPush()
        {

            CircularBuffer testBuffer = new CircularBuffer(10);
            // Exception
            testBuffer.Push(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TooBigPop()
        {

            // Arrange
            CircularBuffer testBuffer = new CircularBuffer(10);
            // Fill with data
            short[] array1 = { 1, 2, 3, 4 };
            short[] array2 = { 5, 6, 7, 8, 9 };
            // Act
            testBuffer.Push(array2);
            testBuffer.Push(array1);
            //exception
            var data = testBuffer.Pop(10);
            // Assert
        }
    }
}

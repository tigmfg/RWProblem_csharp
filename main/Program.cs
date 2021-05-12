using System;
using System.Threading;

namespace 读者_写者问题
{ 
    class Program
    {
        static void Main(string[] args)
        {
            ReaderWriterQue_Nomal book = new ReaderWriterQue_Nomal();
            //ReaderWriterQue_Reader book = new ReaderWriterQue_Reader();
            //ReaderWriterQue_Writer book = new ReaderWriterQue_Writer();
            Thread writer = new Thread(new ThreadStart(book.write));
            writer.Name = "作者";
            Thread reader1 = new Thread(new ThreadStart(book.read));
            reader1.Name = "读者1";
            Thread reader2 = new Thread(new ThreadStart(book.read));
            reader2.Name = "读者2";
            Thread reader3 = new Thread(new ThreadStart(book.read));
            reader3.Name = "读者3";
            writer.Start();
            reader1.Start();
            reader2.Start();
            reader3.Start();
            writer.Join();
            reader1.Join();
            reader2.Join();
            reader3.Join();
        }
    }
}

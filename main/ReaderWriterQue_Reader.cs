using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace 读者_写者问题
{
    class ReaderWriterQue_Reader
    {
        Semaphore book = new Semaphore(1, 1);
        Semaphore readers = new Semaphore(1,1);
        int readerCount = 0;
        int chapter = 0;

        public ReaderWriterQue_Reader()
        {
        }

        public void write()
        {
            while (true)
            {
                Console.WriteLine(Thread.CurrentThread.Name + "正在获取图书");
                book.WaitOne();
                chapter++;
                Console.WriteLine(Thread.CurrentThread.Name + "获得图书");
                Console.WriteLine(Thread.CurrentThread.Name + "正在编写第" + chapter.ToString() + "章");
                Thread.Sleep(500);
                Console.WriteLine(Thread.CurrentThread.Name + "放回图书");
                book.Release();
            }
        }

        public void read()
        {
            while (true)
            {
                if (chapter != 0) 
                {
                    Console.WriteLine(Thread.CurrentThread.Name + "正在获取图书");
                    readers.WaitOne();
                    readerCount++;
                    if (readerCount == 1)
                        book.WaitOne();  //第一个读者占用写者的信号量
                    readers.Release();  //让其他正在等待的读者进入
                    Console.WriteLine(Thread.CurrentThread.Name + "获得图书");
                    Console.WriteLine(Thread.CurrentThread.Name + "正在阅读...");
                    Thread.Sleep(500);
                    Console.WriteLine(Thread.CurrentThread.Name + "放回图书");
                    readers.WaitOne();
                    readerCount--;
                    if (readerCount == 0)  
                        book.Release();  //最后一个读者释放写的资源
                    readers.Release();
                }
            }
        }
    }
}

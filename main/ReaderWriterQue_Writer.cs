using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace 读者_写者问题
{
    class ReaderWriterQue_Writer
    {
        Semaphore book = new Semaphore(1, 1);
        bool isWrite = false;
        int chapter = 0;

        public ReaderWriterQue_Writer()
        {
        }

        public void write()
        {
            while (true)
            {
                Console.WriteLine(Thread.CurrentThread.Name + "正在获取图书");
                isWrite = true;  //当写者要编写时，标记编写状态
                book.WaitOne();  //写者占用读者资源
                chapter++;
                Console.WriteLine(Thread.CurrentThread.Name + "获得图书");
                Console.WriteLine(Thread.CurrentThread.Name + "正在编写第" + chapter.ToString() + "章");
                Thread.Sleep(500);
                Console.WriteLine(Thread.CurrentThread.Name + "放回图书");
                isWrite = false;
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
                    book.WaitOne();
                    if (isWrite)   //当写者要编写时，立马释放资源
                    {
                        book.Release();
                        while (isWrite) { }   //当编写结束，再次接管资源
                        book.WaitOne();
                    }
                    Console.WriteLine(Thread.CurrentThread.Name + "获得图书");
                    Console.WriteLine(Thread.CurrentThread.Name + "正在阅读...");
                    Thread.Sleep(500);
                    Console.WriteLine(Thread.CurrentThread.Name + "放回图书");
                    book.Release();
                }
            }
        }
    }
}

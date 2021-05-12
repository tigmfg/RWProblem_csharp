using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace 读者_写者问题
{
    class ReaderWriterQue_Nomal
    {
        Semaphore book = new Semaphore(1, 1);
        int chapter = 0;

        public ReaderWriterQue_Nomal()
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
                if (chapter != 0)  //写者写了第一章才能读
                {
                    Console.WriteLine(Thread.CurrentThread.Name + "正在获取图书");
                    book.WaitOne();
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

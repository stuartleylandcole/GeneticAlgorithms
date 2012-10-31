using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithms
{
  class CustomQueue
  {
    private int[] array;
    private int head;
    private int tail;
    private int size;
    private int count;

    public CustomQueue(int size)
    {
      array = new int[size];
      head = 0;
      tail = 0;
      count = 0;
      this.size = size;
    } // constructor

    public void enqueue(int value)
    {
      if (!isFull())
      {
        array[tail] = value;
        tail = (tail + 1) % size;
        count++;
      }
    } // enqueue

    public int dequeue()
    {
      if (!isEmpty())
      {
        int result = array[head];
        head = (head + 1) % size;
        count--;
        return result;
      }
      return 0;
    } // dequeue

    public bool isFull()
    {
      return count == size;
    } // isFull

    public bool isEmpty()
    {
      return count == 0;
    } // isEmpty

    public int get(int index)
    {
      return array[index];
    } // get

    public int getCount()
    {
      return count;
    } // getCount
  }
}

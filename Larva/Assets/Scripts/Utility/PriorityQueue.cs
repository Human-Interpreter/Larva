using System;
using System.Collections.Generic;

/// <summary>
/// Priority Queue (우선순위 큐)
/// </summary>
/// <typeparam name="T">우선순위 자료형</typeparam>
/// <typeparam name="U">값 자료형</typeparam>
public class PriorityQueue<T, U>
{
    /// <summary>
    /// 우선순위와 값을 보관하는 구조체
    /// </summary>
    public struct Pair
    {
        public T Priority;
        public U Value;

        public Pair(T priority, U value)
        {
            Priority = priority;
            Value = value;
        }
    }

    /// <summary>
    /// 우선순위 비교 함수
    /// </summary>
    /// <example>
    /// 우선순위 오름차순인 경우
    /// <code>
    /// (a, b) => a.Priority < b.Priority
    /// </code>
    /// </example>
    /// <param name="a">Pair A (부모, 상위 노드)</param>
    /// <param name="b">Pair B (자식, 하위 노드)</param>
    /// <returns></returns>
    public delegate bool Compare(Pair a, Pair b);

    /// <summary>
    /// 우선순위에 따라 데이터를 보관
    /// </summary>
    private List<Pair> data = new List<Pair>();

    /// <summary>
    /// 우선순위 비교 함수 보관
    /// </summary>
    private Compare compare;

    /// <summary>
    /// Priority Queue 생성
    /// </summary>
    /// <param name="compare">우선순위 비교 함수</param>
    public PriorityQueue(Compare compare)
    {
        this.compare = compare;
    }

    /// <summary>
    /// 데이터가 있는지 없는지 반환
    /// </summary>
    /// <returns>데이터가 없다면 true, 하나 이상 존재하는 경우 false</returns>
    public bool IsEmpty()
    {
        return data.Count == 0;
    }

    /// <summary>
    /// 데이터 추가
    /// </summary>
    /// <param name="priority">우선순위</param>
    /// <param name="value">보관할 값</param>
    public void Push(T priority, U value)
    {
        data.Add(new Pair(priority, value));
        int count = data.Count;
        if (count == 1) return;

        int current = count - 1;
        int parent;

        while (current > 0)
        {
            parent = (current - 1) / 2;

            if (compare(data[parent], data[current])) break;
            Swap(parent, current);
            
            current = parent;
        }
    }

    /// <summary>
    /// 가장 우선순위가 높은(혹은 낮은) 데이터를 하나 꺼냄
    /// </summary>
    /// <exception cref="System.Exception">데이터가 없을 때 예외가 발생</exception>
    /// <returns>우선순위와 값을 Pair로 반환</returns>
    public Pair Pop()
    {
        int count = data.Count;
        if (count == 0) throw new Exception("The data has no values");
        
        var result = data[0];
        var last = data[count - 1];

        data.RemoveAt(count - 1);
        if (count - 1 <= 0) return result;
        
        data[0] = last;
        int current = 0;
        int child = 1;

        while (child < count - 2)
        {
            if (child < count - 3 && compare(data[child + 1], data[child])) child ++;
            if (compare(data[current], data[child])) break;
            Swap(current, child);
                
            current = child;
            child = current * 2 + 1;
        }

        return result;
    }

    /// <summary>
    /// 데이터 리스트에 있는 두 요소의 위치를 교환하는 함수
    /// (내부적으로 우선순위에 따라 정렬할 때 사용)
    /// </summary>
    /// <param name="a">Pair A Index</param>
    /// <param name="b">Pair B Index</param>
    private void Swap(int a, int b)
    {
        (data[a], data[b]) = (data[b], data[a]);
    }
}

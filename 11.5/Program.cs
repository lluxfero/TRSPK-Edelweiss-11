﻿using System.Text.RegularExpressions;

string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
StreamWriter triqualog = new StreamWriter(desktopPath + "/tri_qua_log.txt");
StreamWriter generallog = new StreamWriter(desktopPath + "/general_log.txt");
Random rnd = new();
for (int i = 1; i < 6; i++)
{
    int[] arr0 = { rnd.Next(-20, 20), rnd.Next(-20, 20), rnd.Next(-20, 20) };
    int[] arr1 = { rnd.Next(-20, 20), rnd.Next(-20, 20), rnd.Next(-20, 20), rnd.Next(-20, 20) };
    int[] arr2 = { rnd.Next(-20, 20) };

    try
    {
        Triangle tri = new(arr0);
    }
    catch(TriangleException) 
    { 
        triqualog.WriteLine(DateTime.Now + $" tri сatch в {i} цикле");
        generallog.WriteLine(DateTime.Now + $" tri сatch в {i} цикле");
    }


    try
    {
        Quadrangle qua = new(arr1);
    }
    catch (QuadrangleException)
    { 
        triqualog.WriteLine(DateTime.Now + $" qua сatch в {i} цикле");
        generallog.WriteLine(DateTime.Now + $" qua сatch в {i} цикле");
    }

    try
    {
        Circle cir = new(arr2);
    }
    catch(CircleException) 
    { 
        generallog.WriteLine(DateTime.Now + $" cir сatch в {i} цикле"); 
    }
}
triqualog.Close();
generallog.Close();



class Triangle
{
    public int[] coordinates { get; set; } = { 0, 0, 0 };
    public Triangle (int[] c)
    {
        if (c.Length == 3 & c[0] + c[1] > c[2] & c[1] + c[2] > c[0] 
            & c[2] + c[0] > c[1] & c[0] > 0 & c[1] > 0 & c[2] > 0) coordinates = c;
        else throw new TriangleException("Невозможно создать треугольник: неправильно введены длины сторон", c);
    }
}
class Quadrangle
{
    public int[] coordinates { get; set; } = { 0, 0, 0, 0 };
    public Quadrangle(int[] c)
    {
        if (c.Length == 4 & c[0] + c[1] + c[2] > c[3] & c[1] + c[2] + c[3] > c[0]
            & c[2] + c[3] + c[0] > c[1] & c[3] + c[0] + c[1] > c[2] 
            & c[0] > 0 & c[1] > 0 & c[2] > 0 & c[3] > 0) coordinates = c;
        else throw new QuadrangleException("Невозможно создать четырехугольник: неправильно введены длины сторон", c);
    }
}

class Circle
{
    public int[] coordinates { get; set; } = { 0 };
    public Circle(int[] c)
    {
        if (c.Length == 1 & c[0] > 0) coordinates = c;
        else throw new CircleException("Невозможно создать окружность: неправильно введен радиус", c);
    }
}

class GeometryException : Exception
{
    public int[] Parameters { get; private set; }
    public GeometryException(string message, int[] array) : base(message)
    {
        Parameters = array;
    }
}

class TriangleException : GeometryException
{
    public TriangleException(string message, int[] array) : base(message, array) { }
}

class QuadrangleException : GeometryException
{
    public QuadrangleException(string message, int[] array) : base(message, array) { }
}

class CircleException : GeometryException
{
    public CircleException(string message, int[] array) : base(message, array) { }
}
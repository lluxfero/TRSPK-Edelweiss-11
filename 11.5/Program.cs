using System.Text.RegularExpressions;

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
    catch(TriangleException ex) 
    { 
        triqualog.WriteLine(DateTime.Now + $" {ex.Message}: {ex.Parameters[0]} {ex.Parameters[1]} {ex.Parameters[2]}");
        generallog.WriteLine(DateTime.Now + $" {ex.Message}: {ex.Parameters[0]} {ex.Parameters[1]} {ex.Parameters[2]}");
    }


    try
    {
        Quadrangle qua = new(arr1);
    }
    catch (QuadrangleException ex)
    { 
        triqualog.WriteLine(DateTime.Now + $" {ex.Message}: {ex.Parameters[0]} {ex.Parameters[1]} {ex.Parameters[2]} {ex.Parameters[3]}");
        generallog.WriteLine(DateTime.Now + $" {ex.Message}: {ex.Parameters[0]} {ex.Parameters[1]} {ex.Parameters[2]} {ex.Parameters[3]}");
    }

    try
    {
        Circle cir = new(arr2);
    }
    catch(CircleException ex) 
    { 
        generallog.WriteLine(DateTime.Now + $" {ex.Message}: {ex.Parameters[0]}"); 
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
using System;
using NUnit.Framework;

public class calculateArea
{
  /*
  Функция IsValueNumber проверяет, что массив string можно перевести в неотрицательный массив чисел
  */
   public bool IsValueNumber(string[] values)
        {
          double cup = 0;
          foreach (string value in values)
          {
            bool flag = double.TryParse(value, out cup);
            if (!flag || cup <= 0)
              return false;
          }
          return true;
        }
        
    /*
  Функция DoesTriangleExist проверяет, что треугольник с заданными сторонами существует
  */
    public bool DoesTriangleExist(double[] values)
    {
      if (values[0]+values[1]>values[2] && values[0]+values[2]>values[1] &&values[2]+values[1]>values[0])
        return true;
      else
        return false;
    }
    
    /*
  Функция RightAngledTriangle проверяет является ли треугольник прямоугольным
  */
    public bool RightAngledTriangle(double[] values)
    {
      for (int i = 0; i < 3; i++)
      {
        if (Math.Pow(values[i],2) == Math.Pow(values[(i+1)%3],2) + Math.Pow(values[((i+2)%3)],2))
          return true;
      }
      return false;
    }
    
	
	
    /*
  Функция AreaCalculator вычисляет площадь фигуры, если данные значения проходят все проверки
  Возвращает площадь фигуры или 0, если значения не прошли проверки
  
  Принимает значения фигуры через пробел в одну строку
  Для добавления новой фигуры в switch(NumOfValues) нужно добавить новый case 
  с другим количеством данных значений
  */
    public double AreaCalculator(string givenValue = "default")
    {
        var obj = new calculateArea();
		string valueLine = "";
		if (givenValue == "default")
		{
			Console.WriteLine("Введите измерения фигуры через пробел:");
        	valueLine = Console.ReadLine();
		}
		else
		{
			valueLine = givenValue;
		}
        
        Console.WriteLine(valueLine);
        string[] values = valueLine.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        // Проверка введенных значений
        if (!obj.IsValueNumber(values))
        {
          Console.WriteLine("\nОщибка! Можно вводить только положительные числа и пробелы\n");
			return 0;
        }
        else
        {
          
          double[] NumValues = Array.ConvertAll(values, s => double.Parse(s));
          // Выбор формулы для вычисления площади зависит от количества данных значений
          int NumOfValues = NumValues.Length;
          double FigureArea = 0.0;
          // Выбирает формулу для Вычисления площади
          switch(NumOfValues)
          {
            // Вычисление площади круга по формуле S = pi * r^2 (pi = 3.1428, r - радиус)
            case 1:
              FigureArea = 3.1428 * Math.Pow(NumValues[0],2);
              break;
            // Вычисление площади треугольника по формуле Герона
            case 3:
              // Проверка на существование треугольника
              if (!obj.DoesTriangleExist(NumValues))
              {
                Console.WriteLine("\nОщибка! Такой треугольник не существует\n");
                return 0;
              }
              
              double HalfPerimeter = (NumValues[0]+NumValues[1]+NumValues[2]) / 2.0;
              
              FigureArea = Math.Sqrt(HalfPerimeter*(HalfPerimeter - NumValues[0])* (HalfPerimeter - NumValues[1])*(HalfPerimeter - NumValues[2]));
              if (obj.RightAngledTriangle(NumValues))
                Console.WriteLine("Данный треугольник прямоугольный");
              else
                Console.WriteLine("Данный треугольник НЕ прямоугольный");
              break;
            default:
              Console.WriteLine("\nОщибка! Нет формулы для данного количества чисел\n");
              return 0;
              //break;
          }
          Console.WriteLine($"Площадь фигуры: {FigureArea}\n");
			return FigureArea;
        }
    }
}


[TestFixture]
public class UnitTests
{
	calculateArea obj = new calculateArea();
	[Test]
	[TestCase(new string[]{"1"}, true)]
	[TestCase(new string[]{"1","2"}, true)]
	[TestCase(new string[]{"1","-1"}, false)]
	[TestCase(new string[]{"1","we"}, false)]
	public void IsValueNumberTest(string[] values, bool expected)
	{
		bool result = obj.IsValueNumber(values);
		Assert.AreEqual(expected, result);
	}
	
	[Test]
	[TestCase(new double[]{8,4,5}, true)]
	[TestCase(new double[]{8,4,1}, false)]
	[TestCase(new double[]{10,4,5}, false)]
	public void DoesTriangleExistTest(double[] values, bool expected)
	{
		bool result = obj.DoesTriangleExist(values);
		Assert.AreEqual(expected, result);
	}
	
	[Test]
	[TestCase(new double[]{8,6,10}, true)]
	[TestCase(new double[]{8,4,1}, false)]
	[TestCase(new double[]{10,4,5}, false)]
	public void RightAngledTriangleTest(double[] values, bool expected)
	{
		bool result = obj.RightAngledTriangle(values);
		Assert.AreEqual(expected, result);
	}
	
	[Test]
	[TestCase("2", 12.5712)]
	[TestCase("8 4 5", 8.18153408597679)]
	[TestCase("-1", 0)]
	public void AreaCalculatorTest(string values,double expected)
	{
		double result = obj.AreaCalculator(values);
		Assert.AreEqual(expected, result);
	}
}

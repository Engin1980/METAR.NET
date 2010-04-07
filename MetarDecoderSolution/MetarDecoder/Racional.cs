using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetarDecoder
{
  /// <summary>
  /// Represents racional number (struct).
  /// </summary>
  public struct Racional : IComparable<Racional>
  {
    /// <summary>
    /// Represents numerator.
    /// </summary>
    public readonly int Numerator;
    /// <summary>
    /// Represents denominator.
    /// </summary>
    public readonly int Denominator;

    /// <summary>
    /// Initializes a new Instance of MetarDecoder.Racional
    /// </summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    public Racional(int numerator, int denominator)
    {
      if (denominator == 0) throw new Exception("Denominator cannot be 0.");

      this.Numerator = numerator;
      this.Denominator = denominator;
    }

    /// <summary>
    /// Returns real value of racional number.
    /// </summary>
    public double Value
    {
      get
      {
        return Numerator / Denominator;
      }
    }

    /// <summary>
    /// Operator ==
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator ==(Racional a, Racional b)
    {
      bool ret =
        a.Value == b.Value;
      return ret;
    }
    /// <summary>
    /// Operator !=
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator !=(Racional a, Racional b)
    {
      return (!(a == b));
    }
    /// <summary>
    /// Operator greater-than
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator >(Racional a, Racional b)
    {
      return (a.CompareTo(b) > 0);
    }
    /// <summary>
    /// Operator less-than
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator <(Racional a, Racional b)
    {
      return (a.CompareTo(b) < 0);
    }
    /// <summary>
    /// Operator equal-or-more-than
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator >=(Racional a, Racional b)
    {
      return (a.CompareTo(b) >= 0);
    }
    /// <summary>
    /// Operator equal-or-less-than
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator <=(Racional a, Racional b)
    {
      return (a.CompareTo(b) <= 0);
    }


    /// <summary>
    /// Indicates whether this instance and a specified object are equal by their real values.
    /// </summary>
    /// <param name="obj">Another object to compare to.</param>
    /// <returns>true if <paramref name="obj"/> and this instance have the same real value; otherwise, false.</returns>
    /// <filterpriority>2</filterpriority>
    public override bool Equals(object obj)
    {
      if (!(obj is Racional))
        throw new InvalidCastException("Requested type: Racional. Found: " + obj.GetType().FullName);
      else
        return (this == (Racional)obj);
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    /// <filterpriority>2</filterpriority>
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
    /// <summary>
    /// Returns value of this instance; param decides if delimited as racional number (e.g. 3/1).
    /// </summary>
    /// <param name="useFrictionWhenInteger">True when allways use /, false otherwise.</param>
    /// <returns></returns>
    public string ToString(bool useFrictionWhenInteger)
    {
      if (((Denominator == 1) || (Denominator == -1))
        &&
        !useFrictionWhenInteger)
        return Value.ToString();
      else
        return Numerator.ToString() + "/" + Denominator.ToString();
    }
    /// <summary>
    /// Returns value of this instance, allways delimited as racional number (e.g. 3/1).
    /// </summary>
    /// <returns></returns>
    /// <filterpriority>2</filterpriority>
    public override string ToString()
    {
      return ToString(true);
    }

    /// <summary>
    /// Operator plus
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Racional operator + (Racional a, Racional b)
    {
      Racional ret;
      if (a.Denominator == b.Denominator)
      {
        ret = new Racional(a.Numerator + b.Numerator, a.Denominator);
      }
      else
      {
        int dom = a.Denominator*b.Denominator;
        int num = a.Numerator*(dom/a.Denominator) + 
          b.Numerator*(dom/b.Denominator);
        ret = new Racional (num, dom);
      }

      ret = ret.Abbreviate();
      return ret;
    }
    /// <summary>
    /// Operator plus
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Racional operator +(Racional a, int b)
    {
      Racional ret = new Racional(
        a.Numerator + b * a.Denominator, a.Denominator);
      ret = ret.Abbreviate();
      return ret;
    }
    /// <summary>
    /// Operator plus
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Racional operator -(Racional a, Racional b)
    {
      Racional ret;
      if (a.Denominator == b.Denominator)
        ret = new Racional(a.Numerator - b.Numerator, a.Denominator);
      else
      {
        int dom = a.Denominator * b.Denominator;
        int num = a.Numerator * (dom / a.Denominator) -
          b.Numerator * (dom / b.Denominator);
        ret = new Racional(num, dom);
      }
      ret = ret.Abbreviate();
      return ret;
    }
    /// <summary>
    /// Operator minus
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Racional operator -(Racional a, int b)
    {
      Racional ret = new Racional(
        a.Numerator - b * a.Denominator, a.Denominator);
      ret = ret.Abbreviate();
      return ret;
    }
    /// <summary>
    /// Operator minus
    /// </summary>
    /// <param name="b"></param>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Racional operator -(int b, Racional a)
    {
      Racional ret = new Racional(
        b * a.Denominator - a.Numerator, a.Denominator);
      ret = ret.Abbreviate();
      return ret;
    }
    /// <summary>
    /// Operator multiply
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Racional operator *(Racional a, Racional b)
    {
      Racional ret;
        int dom = a.Denominator * b.Denominator;
        int num = a.Numerator * b.Denominator;
        ret = new Racional(num , dom );
        ret = ret.Abbreviate();
      return ret;
    }
    /// <summary>
    /// Operator multiply
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Racional operator *(Racional a, int b)
    {
      Racional ret = new Racional(
        a.Numerator*b, a.Denominator);
      ret = ret.Abbreviate();
      return ret;
    }
    /// <summary>
    /// Operator divide
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Racional operator /(Racional a, Racional b)
    {
      Racional ret;
      int num = a.Numerator * b.Denominator;
      int dom = a.Denominator * b.Numerator;
        ret = new Racional(num , dom );

        ret = ret.Abbreviate();
      return ret;
    }
    /// <summary>
    /// Operator divide
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Racional operator /(Racional a, int b)
    {
      Racional ret = a / new Racional(b, 1);
      ret = ret.Abbreviate();
      return ret;
    }
    /// <summary>
    /// Operator divide
    /// </summary>
    /// <param name="b"></param>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Racional operator /(int b, Racional a)
    {
      Racional ret = new Racional(b, 1) / a;
      ret = ret.Abbreviate();
      return ret;
    }

    /// <summary>
    /// Operator implicit conversion to double
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static implicit operator double (Racional a)
    {
      return a.Value;
    }
    /// <summary>
    /// Operator explicit conversion to integer.
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static explicit operator int(Racional a)
    {
      return (int)a.Value;
    }
    /// <summary>
    /// Operator implicit conversion from integer.
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static implicit operator Racional(int a)
    {
      return new Racional(a, 1);
    }
    /// <summary>
    /// Creates value from double.
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static Racional FromDouble(double val)
    {
      Racional ret;

      int mult = 1;
      int intVal = (int)val;

      while (val != intVal)
      {
        mult *= 10;
        intVal = (int)(val*mult);
      }

      ret = new Racional(intVal, mult);
      ret.Abbreviate();
      return ret;
    }

    /// <summary>
    /// Abbreviates the racional. E.g. from 2/4 to 1/2.
    /// </summary>
    /// <returns></returns>
    public Racional Abbreviate()
    {
      Racional ret;
      int dom = Denominator;
      int num = Numerator;
      int nsd = GetGreatestCommonDivisor(dom, num);
      if (dom < 0)
        nsd = -nsd;
      ret = new Racional(num / nsd, dom / nsd);
      return ret;
    }

    private static int GetGreatestCommonDivisor(int u, int v)
    {
//      Mějme dána dvě přirozená čísla, uložená v proměnných u a v (u>v).
//Dokud v není nulové, opakuj:
//  Do r ulož zbytek po dělení čísla u číslem v
//  Do u ulož v
//  Do v ulož r
//Konec algoritmu, v u je uložen největší společný dělitel původních čísel

      //      Mějme dána dvě přirozená čísla, uložená v proměnných u a v (u>v).
      if (!(u > v))
      {
        int pom = v;
        v = u;
        u = pom;
      }

      int r;
      //Dokud v není nulové, opakuj:
      while (v > 0)
      {
        //  Do r ulož zbytek po dělení čísla u číslem v
        r = u % v;
        //  Do u ulož v
        u = v;
        //  Do v ulož r
        v = r;
      }
      //Konec algoritmu, v u je uložen největší společný dělitel původních čísel

      return u;
    }

    #region IComparable<Racional> Members

    /// <summary>
    /// Compares two racional number by their real values.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(Racional other)
    {
      if (this.Value > other.Value)
        return 1;
      else if (this.Value < other.Value)
        return -1;
      else
        return 0;
    }

    #endregion
  }
}

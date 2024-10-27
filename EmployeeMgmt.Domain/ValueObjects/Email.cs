using System;
using System.Text.RegularExpressions;

namespace EmployeeMgmt.Domain.ValueObjects {
  public class Email {
    public string Value { get; }

    public Email(string value) {

      if (string.IsNullOrEmpty(value) || !IsValidEmail(value)) {
        throw new ArgumentException("Invalid email address");
      }

      Value = value;
    }


    private bool IsValidEmail(string email)
    {
      var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$"); 
      return emailRegex.IsMatch(email);
    }


    public override bool Equals(object? obj) {
      if (obj is Email other) {
        return Value == other.Value;
      }

      return false;
    }


    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;
  }
}
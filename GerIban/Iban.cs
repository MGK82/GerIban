using System;
using System.Text.RegularExpressions;

namespace GerIban {
  enum Spaces { with, without };

  class Iban {
    const string GER = "1314";
    string _blz, _knr;

    public Iban(string blz, string knr) {
      if(!Regex.IsMatch(blz, "^[0-9]{8}$")) {
        throw new ArgumentException("", "blz");
      }

      if(!Regex.IsMatch(knr, "^[0-9]{1,10}$")) {
        throw new ArgumentException("", "knr");
      }

      _blz = blz;
      _knr = knr.PadLeft(10, '0');
    }

    public string ToString(Spaces format) {
      if(Spaces.with == format) {
        return ToStringWithSpaces();
      }

      return ToString();
    }

    private string ToStringWithSpaces() {
      return Regex.Replace(ToString(), ".{4}", "$0 ");
    }

    public override string ToString() {
      return "DE" + GetChecknum() + _blz + _knr;
    }

    private string GetChecknum() {
      return (98 - (decimal.Parse(_blz + _knr + GER + "00") % 97)).ToString("00");
    }
  }
}

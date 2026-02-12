namespace Lab7.Purple
{
  public class Task1
  {
    public struct Participant
    {
      private string _name; /* basic fields */
      private string _surname;
      private double[] _coefs;
      private int[] _marks;
      public double _totalScore;

      public string Name => _name; /* getters */
      public string Surname => _surname;
      public double[] Coefs => _coefs;
      public int[] Marks => _marks;
/* TotalScore optimized for calculating only when nessesary, not every time we ask for it */
      public double TotalScore => _totalScore == -1.0 ? _calculateTotalScore() : _totalScore; 
/* setters with updating _totalScore field function */
      public void SetCriterias(double[] coefs) { _coefs = (double[])coefs.Clone(); _calculateTotalScore(); } 
      public void Jump(int[] marks){ _marks = (int[])marks.Clone(); _calculateTotalScore(); }

/* operators overload for overall struct usage improvements and Array.Sort availability */
      public static bool operator <(Participant left, Participant right) => left.TotalScore < right.TotalScore;
      public static bool operator >(Participant left, Participant right) => left.TotalScore > right.TotalScore;
      public static bool operator <=(Participant left, Participant right) => left.TotalScore <= right.TotalScore;
      public static bool operator >=(Participant left, Participant right) => left.TotalScore >= right.TotalScore;

      public Participant(string _name, string _surname)
      {
	this._name = _name;
	this._surname = _surname;
	_coefs = new double[4] {2.5, 2.5, 2.5, 2.5};
	_marks = new int[4] {0, 0, 0, 0};
	_totalScore = -1.0;
      }

/* private helper for optimizing _totalScore calculation */
      private double _calculateTotalScore() 
      {
	double sum = 0.0;
	int worstInd = 0;
	int bestInd = 0;
	for (int i = 0; i < 4; i++){
	  if (_marks[i] < _marks[worstInd]) worstInd = i;
	}
	for (int i = 0; i < 4; i++){
	  if (_marks[i] > _marks[bestInd] && i != worstInd) bestInd = i;
	}
	for (int i = 0; i < 4; i++){
	  if (i != bestInd && i != worstInd){
	    sum+= _marks[i] * _coefs[i];
	  }
	}
	return sum;
      }

      public void Print()
      {
	Console.Write($"Name: {Name}\nSurname: {Surname}\nTotal score: {TotalScore}\n");
      }

      public static void Sort(Participant[] array) { Array.Sort(array); }
    }
  }
}

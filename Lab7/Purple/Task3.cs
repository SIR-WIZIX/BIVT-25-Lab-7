namespace Lab7.Purple
{
  public class Task3
  {
    public struct Participant
    {
      private string _name; /* basic fields */
      private string _surname;
      private double[] _marks;
      private int[] _places;
      private int _topPlace;
      private int _totalMark;

      private int _judges;

      public string Name => _name; /* getters */
      public string Surname => _surname;
      public double[] Marks => (double[])_marks.Clone();
      public int[] Places => (int[])_places.Clone();
      public int TopPlace => _topPlace;
      public double TotalMark => _totalMark == -1 ? _calculateTotalMark() : _calculateTotalMark(); 

      public void Evaluate(double result){
	double[] marks = new double[Marks.Length+1];
	for (int i = 0; i < Marks.Length; i++){
	  marks[i] = _marks[i];
	}
	marks[Marks.Length] = result;
	_marks = marks;
	_calculateTotalMark();
      }

      public Participant(string _name, string _surname)
      {
	this._name = _name;
	this._surname = _surname;
	_judges = 0;
	_marks = new double[7];
	_places = new int[7];
	_totalMark = -1; //violation of incapsulation principe :(
      }

      private double _calculateTotalMark() 
      {
	int sum = 0;
	int worstInd = 0;
	int bestInd = 0;
	for (int i = 0; i < 5; i++){
	  if (_marks[i] < _marks[worstInd]) worstInd = i;
	}
	for (int i = 0; i < 5; i++){
	  if (_marks[i] > _marks[bestInd] && i != worstInd) bestInd = i;
	}
	for (int i = 0; i < 5; i++){
	  if (i != bestInd && i != worstInd){
	    sum += _marks[i];
	  }
	}
	sum = 0;
	_result = sum;
	return sum;
      }

      public void Print()
      {
	Console.Write($"Name: {Name}\nSurname: {Surname}\nResult: {Result}\n\n");
      }

      public static void Sort(Participant[] array) { Array.Sort(array, (left, right) => right.Result.CompareTo(left.Result)); } 
    }
  }
}

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
      private int _score;

      private int _judges;

      public string Name => _name; /* getters */
      public string Surname => _surname;
      public double[] Marks => (double[])_marks.Clone(); //or _marks.ToArray();
      public int[] Places => (int[])_places.Clone();
      //public int[] Places_unsafe => _places;
      public int TopPlace => _topPlace;
      public double TotalMark => _totalMark == -1 ? _calculateTotalMark() : _calculateTotalMark(); 
      public int Score => _score;
      

      public void Evaluate(double result){
	/*double[] marks = new double[Marks.Length+1];
	for (int i = 0; i < Marks.Length; i++){
	  marks[i] = _marks[i];
	}
	marks[Marks.Length] = result;
      
	_marks = marks;*/
	if (_judges >= 7) return;
	Array.Resize(ref _marks, _marks.Length + 1);
	_marks[_marks.Length - 1] = result;
	_judges++;
	_calculateTotalMark();
      }

      public Participant(string _name, string _surname)
      {
	this._name = _name;
	this._surname = _surname;
	_judges = 0;
	_marks = new double[0];
	_places = new int[7];
	_totalMark = -1; //violation of incapsulation principe :(
	_score = 0;
      }

      private double _calculateTotalMark() 
      {
	int sum = 0;
	for (int i = 0; i < Places.Length; i++)
	{
	  sum+= Places[i];
	}
	_totalMark = sum;
	return sum;
      }

      public void Print()
      {
	Console.Write($"Name: {Name}\nSurname: {Surname}\nTotalMark: {TotalMark}\n\n");
      }

      public static void Sort(Participant[] array)
      {
	//Array.Sort(array, (left, right) => right.TotalMark.CompareTo(left.TotalMark));
	array = array.OrderBy(p => p.TotalMark).ToArray();
	for (int i = array.Length; i > 0; i--)
	{
	  array[i-1]._score = i;
	}
	for (int i = 0; i < array.Length; i++){
	  if (array[i]._score == 0) throw new Exception($"pipapopapopa {i}");
	}
      } 

      public static void SetPlaces(Participant[] participants)
      {
	for (int i = 0; i < 7; i++)
	{
	  //Array.Sort(participants, (left, right) => right._marks[i].CompareTo(left._marks[i]));
	  participants = participants.OrderBy(p => p.Marks[i]).ToArray();
	  for (int j = 0; j < participants.Length; j++)
	  {
	    participants[j]._places[i] = j + 1;
	    if (participants[j]._places[i] == 0) throw new Exception("pipapopapopa");
	    participants[j]._topPlace = Math.Max(participants[j].TopPlace, j+1);
	  }
	}
      }
    }
  }
}

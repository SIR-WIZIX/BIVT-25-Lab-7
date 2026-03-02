using System.Runtime;
using System.Xml.Linq;

namespace Lab7.Purple
{
    public class Task5
    {
      public struct Response
      {
	private string _animal;
	private string _characterTrait;
	private string _concept;

	public string Animal => _animal;
	public string CharacterTrait => _characterTrait;
	public string Concept => _concept;

	public Response (string a, string ch, string c)
	{
	  _animal = a;
	  _characterTrait = ch;
	  _concept = c;
	}

	private bool compare(Response l, Response r)
	{
	  return (l.Concept == r.Concept && l.Animal == r.Animal && l.CharacterTrait == r.CharacterTrait);
	}
	  

	public int CountVotes(Response[] responses, int questionNumber)
	{
	  int s = 0;
	  for (int i = 0; i < responses.Length; i++)
	  {
	    if (compare(responses[i], this))
	    {
	      s+=1;
	    }
	  }
	  return s;
	}

	public void Print()
	{
	  Console.Write($"Animal: {Animal}\nCharacterTrait: {CharacterTrait}\nConcept: {Concept}\n");
	}
      }

      public struct Research
      {
	private string _name;
	private Response[] _responses;

	public string Name => _name;
	public Response[] Responses => (Response[])_responses.Clone();

	public Research(string name)
	{
	  _responses = new Response[0];
	}

	private void Append(ref Response[] rs, Response r)
	{
	  Response[] buff = new Response[rs.Length+1];
	  for (int i = 0; i < rs.Length; i++)
	  {
	    buff[i] = rs[i];
	  }
	  buff[rs.Length] = r;
	  rs = buff;
	}

	public void Add(string[] answers)
	{
	  Append(ref _responses, new Response(answers[0], answers[1], answers[2]));
	}

	public string[] GetTopResponses(int question)
	{
	  Dictionary<string, int> data = new Dictionary<string, int>(0);
	  /*for (int i = 0; i < _responses.Length; i++)
	  {
	    switch (question){
	      case 1:
		if (Responses[i].Animal == null) continue;
		data[Responses[i].Animal] = 0;
		break;
	      case 2:
		if (Responses[i].CharacterTrait == null) break;
		data[Responses[i].CharacterTrait] = 0;
		break;
	      case 3:
		if (Responses[i].Concept == null) break;
		data[Responses[i].Concept] = 0;
		break;
	    }
	  }
	  for (int i = 0; i < _responses.Length; i++)
	  {
	    switch (question){
	      case 1:
		if (Responses[i].Animal == null) break;
		data[Responses[i].Animal]++;
		break;
	      case 2:
		if (Responses[i].CharacterTrait == null) break;
		data[Responses[i].CharacterTrait]++;
		break;
	      case 3:
		if (Responses[i].Concept == null) break;
		data[Responses[i].Concept]++;
		break;
	    }
	  }*/

	  int[] counts = new int[Responses.Length];

	  for (int i = 0; i < Responses.Length; i++){
	    for (int j = 0; j < Responses.Length; j++){
	      switch (question){
		case 1:
		  if (Responses[i].Animal == Responses[j].Animal) counts[i]++;
		  break;
		case 2:
		  if (Responses[i].CharacterTrait == Responses[j].CharacterTrait) counts[i]++;
		  break;
		case 3:
		  if (Responses[i].Concept == Responses[j].Concept) counts[i]++;
		  break;
	      }
	    }
	  }

	  Response[] temp = new Response[0];
	  temp = Responses;

	  int pos = 1;
	  while (pos < counts.Length)
	  {
	    if (counts[pos] <= counts[pos-1])
	    {
	      pos++;
	    }
	    else
	    {
	      (counts[pos], counts[pos-1]) = (counts[pos-1], counts[pos]);
	      (temp[pos], temp[pos-1]) = (temp[pos-1], temp[pos]);
	      if (pos > 1)
	      {
		pos--;
	      }
	    }
	  }
	  
	  string[] ans = new string[5];
	  int found = 0;
	  int ind = 0;
	  string last = "";
	  while (found < 5 && ind < temp.Length)
	  {
	    string cur = "";
	    switch (question){
	      case 1:
		cur = Responses[ind].Animal;
		break;
	      case 2:
		cur = Responses[ind].CharacterTrait;
		break;
	      case 3:
		cur = Responses[ind].Concept;
		break;
	    }
	    if (cur != last){
	      ans[found++] = cur;
	      last = cur;
	    }
	    ind++;
	  }


	  return ans;
	}

	public void Print()
	{
	  Console.Write($"Name: {Name}\nReponses:\n\n");
	  foreach (Response r in Responses)
	  {
	    r.Print();
	  }
	}
      }

      
    }
}

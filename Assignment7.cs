using Assignment7;

namespace Assignment7
{
    public  class Student
    {
         int[] quiz_scores { get; set; } = new int[10];
         int[] hw_scores { get; set; } = new int[10];
         int midterm { get; set; }
         int final_exam_grade { get; set; }
         int quiz_average { get; set; }
         double hw_average { get; set; }
         double ovr_avg { get; set; }
        string name;
        private int id { get; set; }

        public string Name
        {
            get{ return name; }
            set
            {
                name = value;
            }
        }
        public int Id
        {
            get { return id; } set { id = value; }
        }
        public Student(string x)
        {
    
            string[] s = x.Split(",");
            name = s[0];
            id = Convert.ToInt32(s[1]);
            for (int i = 2; i < 12; i++)
            {
                quiz_scores[i - 2] = Convert.ToInt32(s[i]);

            }
            for (int i = 12; i < 22; i++)
            {
                hw_scores[i - 12] = Convert.ToInt32(s[i]);

            }
            midterm = Convert.ToInt32(s[22]);
            final_exam_grade = Convert.ToInt32(s[23]);
            calcQuizAverage();
            calcHwAverage();
        }
        public void calcQuizAverage()
        {
            int lowest = 10000;
            int prev;
            for (int i = 0; i < quiz_scores.Length; i++)
            {
                prev = quiz_scores[i];
                if(prev < lowest)
                {
                    lowest = prev;
                }
            }
            int total = 0;
            for (int j= 0; j < quiz_scores.Length; j++)
            {
                if (quiz_scores[j] != lowest)
                {
                    total = total + quiz_scores[j];
                }
            }
            quiz_average = total / 9;
            
        }
        public void calcHwAverage()
        {
            int lowest = 10000;
            int prev;
            for (int i = 0; i < hw_scores.Length; i++)
            {
                prev = hw_scores[i];
                if (prev < lowest)
                {
                    lowest = prev;
                }
            }
            int total = 0;
            for (int j = 0; j < hw_scores.Length; j++)
            {
                if (hw_scores[j] != lowest)
                {
                    total = total + hw_scores[j];
                }
            }
            hw_average = total / 9;
            
        }
        public void calcOverallAverage()
        {
            double overall = (quiz_average*0.4) + (hw_average*0.1) + (midterm*0.2) + (final_exam_grade*0.3);
            ovr_avg = overall;
           
        }
        public void getGrade()
        {
           for (int i = 0; i < quiz_scores.Length; i++)
            {
                Console.WriteLine("Quiz " + i + ": " + quiz_scores[i]);
            }
            Console.WriteLine("Quiz Avg: " + quiz_average);
            for (int j = 0; j < hw_scores.Length; j++)
            {
                Console.WriteLine("Hw " + j + ": " + hw_scores[j]);
            }
            Console.WriteLine("HW Avg: " +  hw_average);
        }
    }


}



public class GradeBook
{
    static Node head = null;
    public static StringNode stringhead = null;
    public static void addNode(Student newData)
    {
        Node temp = new Node();
        temp.data = newData;
        temp.next = head;
        head = temp;
    }
    public static void addStringNode(string newName)
    {
        StringNode stringtemp = new StringNode();
        stringtemp.data = newName;
        stringtemp.next = stringhead;
        stringhead = stringtemp;
    }

    

    public GradeBook(string file_name)
    {
        try
        {
            StreamReader str1 = new StreamReader(file_name);
            string header = str1.ReadLine();
            while (!str1.EndOfStream)
            {
                string line = str1.ReadLine();
                addNode(new Student(line));

                


            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public Student getStudent(string student_name)
    {
        Node temp = head;
        while (temp != null)
        {
            if (temp.data.Name == student_name)
            {
                return temp.data;
            }
            temp = temp.next;
        }
        return null;
    }
    public void getStudentGrade(string student_name)
    {
        getStudent(student_name).getGrade();
    }
 
    public StringNode getAllStudentNames()
    {
        StringNode tempstring = stringhead;
        Node temp = head;
        int count = 0;
        while ( temp != null)
        {
           addStringNode(temp.data.Name);
          
            count++;
            
            temp = temp.next;
            
        }
        
        return tempstring;

    }
    
 }




    class Assignment7
    {
   
       
       
        static void Main(string[] args)
        {

        Console.WriteLine("Enter the file name:");
        string file = Console.ReadLine();

        StatisticGradeBook stud = new StatisticGradeBook(file);
        ThreadStart ts1 = new ThreadStart(stud.run);
        Thread t1 = new Thread(ts1);
        t1.Start();
        Console.WriteLine("What is the students name: ");
        string stud_name = Console.ReadLine();
        stud.getStudentGrade(stud_name);


        }
       
    }
class Node
{
    public Student data;
    public Node next;
}
public class StringNode
{
    public string data;
    public StringNode next;
}
public class StatisticGradeBook : GradeBook
{

    public StatisticGradeBook(string x) : base(x)
    {

    }
    public void run()
    {
        
       StringNode run = new StringNode();
        run = getAllStudentNames();
        StringNode tempstring = stringhead;
        int count = 0;
        while (tempstring != null)
        {
       
           
            if (count == 100||count==200||count==300||count==400||count==500||count==600||count==700||count==800||count==900) {
                Console.WriteLine("Calculating grades " + count+ " out of 1000");
            }
           
            count++;
            tempstring = tempstring.next;

        }
        Console.WriteLine("All grades calculated ");
        
    }
}




using MPC.Models.DomainModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{



    public class QuestionQueueItem 
    {


      



        private int _ID;
        private string _Name;
        private short _ItemType;
        private string _VisualQuestion;
        //Private _Answer As String
        private string _DefaultAnswer;
        private string _AnswerID;
        private bool _IsAnswered;
        private long _CostCentreID;
        private int _SequenceID;

        private List<CostCentreAnswer> _AnswersTable;
        private double _Qty1Answer;
        private double _Qty2Answer;
        private double _Qty3Answer;
        private double _Qty4Answer;

        private double _Qty5Answer;

     
        /// <summary>
        /// overloaded constructor
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <param name="ItemType"></param>
        /// <param name="VisualQuestion"></param>
        /// <param name="Answer"></param>
        /// <param name="DefaultAnswer"></param>
        /// <param name="AnswerID"></param>
        /// <param name="IsAnswered"></param>
        public QuestionQueueItem(int ID, string Name, long CostCentreID, short ItemType, string VisualQuestion, string DefaultAnswer, string AnswerID, bool IsAnswered, double Qty1Answer, double Qty2Answer = 0,
        double Qty3Answer = 0, double Qty4Answer = 0, double Qty5Answer = 0, List<CostCentreAnswer> answers = null)
        {
            this._ID = ID;
            this._Name = Name;
            this._ItemType = ItemType;
            this._VisualQuestion = VisualQuestion;
            //Me._Answer = Answer
            this._DefaultAnswer = DefaultAnswer;
            this._AnswerID = AnswerID;
            this._IsAnswered = IsAnswered;
            this._CostCentreID = CostCentreID;
            this._AnswersTable = answers;

            this._Qty1Answer = Qty1Answer;
            this._Qty2Answer = Qty2Answer;
            this._Qty3Answer = Qty3Answer;
            this._Qty4Answer = Qty4Answer;
            this._Qty5Answer = Qty5Answer;
        }

        public int ID
        {
            get { return this._ID; }
            set { this._ID = value; }
        }

        public string Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }

        public short ItemType
        {
            get { return this._ItemType; }
            set { this._ItemType = value; }
        }

        public string VisualQuestion
        {
            get { return this._VisualQuestion; }
            set { this._VisualQuestion = value; }
        }


        public string DefaultAnswer
        {
            get { return this._DefaultAnswer; }
            set { this._DefaultAnswer = value; }
        }

        public string AnswerID
        {
            get { return this._AnswerID; }
            set { this._AnswerID = value; }
        }

        public bool IsAnswered
        {
            get { return this._IsAnswered; }
            set { this._IsAnswered = value; }
        }

        public int SequenceID
        {
            get { return this._SequenceID; }
            set { this._SequenceID = value; }
        }

        public long CostCentreID
        {
            get { return this._CostCentreID; }
            set { this._CostCentreID = value; }
        }

        public double Qty1Answer
        {
            get { return this._Qty1Answer; }
            set { this._Qty1Answer = value; }
        }

        public double Qty2Answer
        {
            get { return this._Qty2Answer; }
            set { this._Qty2Answer = value; }
        }

        public double Qty3Answer
        {
            get { return this._Qty3Answer; }
            set { this._Qty3Answer = value; }
        }

        public double Qty4Answer
        {
            get { return this._Qty4Answer; }
            set { this._Qty4Answer = value; }
        }

        public double Qty5Answer
        {
            get { return this._Qty5Answer; }
            set { this._Qty5Answer = value; }
        }

        public List<CostCentreAnswer> AnswersTable
        {
            get { return this._AnswersTable; }
            set { this._AnswersTable = value; }
        }



    }


    

	

}

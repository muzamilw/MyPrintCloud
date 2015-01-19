using MPC.Models.DomainModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{


    public class ExecutionQueueDTO
    {


        /// <summary>
        /// Adds an item in the Execution Queue
        /// Same Questions will be treated as one and will not be asked again
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <param name="Itemtype"></param>
        /// <param name="VisualQuestion"></param>
        /// <param name="Answer"></param>
        /// <param name="DefaultAnswer"></param>
        /// <param name="AnswerID"></param>
        /// <param name="IsAnswered"></param>
        /// <returns></returns>
        public static QueueItemDTO addItem(int ID, string Name, long CostCentreID, short Itemtype, string VisualQuestion, string DefaultAnswer, string AnswerID, bool IsAnswered, List<CostCentreAnswer> AnswersTable, List<ExecutionQueueDTO> QuestionQueue)
        {
            bool bAddItem = true;
            QueueItemDTO newItem = null;
            try
            {
                //add the item if count is zero
                if (QuestionQueue.Count == 0)
                {
                    newItem = new QueueItemDTO(ID, Name, CostCentreID, Itemtype, VisualQuestion, DefaultAnswer, AnswerID, IsAnswered, 0, 0,
                    0, 0, 0, AnswersTable);
                    return newItem;
                }
                else
                {
                    //count isnt zero.. so we have to search the collection and see if it already contains the specific Question
                    // we will ask the Question only once.
                    //Dim item As modal.QueueItem
                    //For Each item In _Items
                    //Item.ID()

                    //Next

                    for (int iCounter = 0; iCounter <= QuestionQueue.Count - 1; iCounter++)
                    {
                        //ExecutionQueueDTO queueItem = QuestionQueue[iCounter];
                        //if (queueItem..item.ID == ID && item.CostCentreID == CostCentreID) 
                        //{
                        //    bAddItem = false;
                        //    break; // TODO: might not be correct. Was : Exit For
                        //}
                        break;
                    }

                    //add the item according to flag situation
                    if (bAddItem == true)
                    {
                        newItem = new QueueItemDTO(ID, Name, CostCentreID, Itemtype, VisualQuestion, DefaultAnswer, AnswerID, IsAnswered, 0, 0,
                        0, 0, 0, AnswersTable);
                        return newItem;
                    }
                    else 
                    {
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public QueueItemDTO Item
        {
            get { return (QueueItemDTO)Item; }

            set { Item = value; }
        }


        //public IList Items
        //{
        //    get { return list; }
        //}




        //'Public Property QueueItems() As Collection
        //'    Get
        //'        Return _Items
        //'    End Get
        //'    Set(ByVal Value As Collection)
        //'        _Items = Value
        //'    End Set
        //'End Property

    }

    public class QueueItemDTO
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
        /// Default constructor
        /// </summary>

        public QueueItemDTO()
        {
        }

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
        public QueueItemDTO(int ID, string Name, long CostCentreID, short ItemType, string VisualQuestion, string DefaultAnswer, string AnswerID, bool IsAnswered, double Qty1Answer, double Qty2Answer = 0,
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


    

	public class CostMatixDTO
	{
		private int _ID;
		private string _Name;
		private string _Description;
        private List<CostCentreMatrixDetail> _MatrixItems = new List<CostCentreMatrixDetail>();
		private int _Rows;

		private int _Columns;
		/// <summary>
		/// Default constructor
		/// </summary>

		public CostMatixDTO()
		{
		}


		#region "Properties"
		public int ID {
			get { return this._ID; }
			set { this._ID = value; }
		}

		public string Name {
			get { return this._Name; }
			set { this._Name = value; }
		}

		public string Description {
			get { return this._Description; }
			set { this._Description = value; }
		}

        public List<CostCentreMatrixDetail> Items
        {
			get { return this._MatrixItems; }
			set { this._MatrixItems = value; }
		}

		public int Rows {
			get { return this._Rows; }
			set { this._Rows = value; }
		}

		public int Columns {
			get { return this._Columns; }
			set { this._Columns = value; }
		}
		#endregion
	}

}

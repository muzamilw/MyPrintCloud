using MPC.Models.DomainModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

        

[Serializable()]
public class CostCentreQueueItem
{

	private long m_CostCentreID;
	private string m_CostCentreName;
	private string m_CostCentreCodeFileName;
	
	private int m_CostCentreSequence;
	private CostCentrePriceResult[] m_CostCentrePriceResult = new CostCentrePriceResult[5];
	private CostCentreCostResult[] m_CostCentreCostResult = new CostCentreCostResult[5];
	private CostCentreActualCostResult[] m_CostCentreActualCostResult;

	private DataSet m_WorkInstructions;
	private string m_WorkInstructionsString1;
	private string m_WorkInstructionsString2;
	private string m_WorkInstructionsString3;
	private string m_WorkInstructionsString4;

	private string m_WorkInstructionsString5;

	private bool m_IsCostCentreExecuted = false;
	private int m_SetupSpoilage;

	private double m_RunningSpoilage;

	public CostCentreQueueItem(long CostCentreID, string CostCentreName, int CostCentreSequence, string CodeFileName, DataSet Workinstructions, int SetupSpoilage, double RunningSpoilage)
	{
		try {
			//Dim oDataSet As DataSet
			//Dim oInstructionRow As DataRow
			//Dim oInstruction As Model.costcentres.Instruction
			//Dim oInstructionOption As Model.costcentres.InstructionOption
			//Dim oOptionRow, oOptionsResults() As DataRow

			m_CostCentreID = CostCentreID;
			
			
			m_CostCentreSequence = CostCentreSequence;
			m_CostCentreCodeFileName = CodeFileName;

			m_SetupSpoilage = SetupSpoilage;
			m_RunningSpoilage = RunningSpoilage;

			//'loading work instructions

			//oDataSet = mys.costcentreengine.CostCentreManager.getCostCentreWorkInstruction(CostCentreID.ToString)

			//Dim iCounter, xCounter As Integer
			//'iterating in the instructions.
			//iCounter = 0

			//ReDim m_WorkInstructions(oDataSet.Tables(0).Rows.Count - 1)

			//For Each oInstructionRow In oDataSet.Tables(0).Rows
			//    oInstruction = New Model.costcentres.Instruction
			//    oInstruction.InstructionID = oInstructionRow.Item("InstructionID").ToString
			//    oInstruction.Instruction = oInstructionRow.Item("Instruction").ToString
			//    oInstruction.CostCentreID = CostCentreID.ToString

			//    'getting options for the instruction
			//    oOptionsResults = oDataSet.Tables(1).Select("InstructionID = " + oInstruction.InstructionID)

			//    xCounter = 0
			//    For Each oOptionRow In oOptionsResults
			//        oInstructionOption = New Model.costcentres.InstructionOption(oOptionRow.Item("Id").ToString, oOptionRow.Item("choice").ToString, oInstruction.InstructionID)
			//        oInstruction.AddOptions(oInstructionOption, oOptionsResults.Length - 1, xCounter)
			//        xCounter += 1
			//    Next

			//    m_WorkInstructions(iCounter) = oInstruction
			//    iCounter += 1
			//Next

			m_WorkInstructions = Workinstructions;

		} catch (Exception ex) {
		}

	}

	public long CostCentreID {
		get { return m_CostCentreID; }
		set { m_CostCentreID = value; }
	}

	public string CostCentreName {
		get { return m_CostCentreName; }
		set { m_CostCentreName = value; }
	}

	

	public int CostCentreSequence {
		get { return m_CostCentreSequence; }
		set { m_CostCentreSequence = value; }
	}


    public CostCentrePriceResult[] MyProperty { get; set; }

    public CostCentrePriceResult[] CostCentrePriceResults
    {
        get { return m_CostCentrePriceResult; }
        set { m_CostCentrePriceResult = value; }
    }

    public CostCentreCostResult[] CostCentreCostResults
    {
        get { return m_CostCentreCostResult; }
        set { m_CostCentreCostResult = value; }
    }


	public CostCentreActualCostResult[] CostCentreActualCostResults {
		get { return m_CostCentreActualCostResult; }
		set { m_CostCentreActualCostResult = value; }
	}

	public bool IsCostCentreExecuted {
		get { return m_IsCostCentreExecuted; }
		set { m_IsCostCentreExecuted = value; }
	}

	public string CodeFilename {
		get { return m_CostCentreCodeFileName; }
		set { m_CostCentreCodeFileName = value; }
	}

	public DataSet WorkInstructions {
		get { return m_WorkInstructions; }
	}

	public string WorkInstructionsString1 {
		get { return m_WorkInstructionsString1; }
		set { m_WorkInstructionsString1 = value; }
	}

	public string WorkinstructionsString2 {
		get { return this.m_WorkInstructionsString2; }
		set { this.m_WorkInstructionsString2 = value; }
	}

	public string WorkinstructionsString3 {
		get { return this.m_WorkInstructionsString3; }
		set { this.m_WorkInstructionsString3 = value; }
	}

	public string WorkinstructionsString4 {
		get { return this.m_WorkInstructionsString4; }
		set { this.m_WorkInstructionsString4 = value; }
	}

	public string WorkinstructionsString5 {
		get { return this.m_WorkInstructionsString5; }
		set { this.m_WorkInstructionsString5 = value; }
	}

	public int SetupSpoilage {
		get { return this.m_SetupSpoilage; }
		set { this.m_SetupSpoilage = value; }
	}

	public double RunningSpoilage {
		get { return this.m_RunningSpoilage; }
		set { this.m_RunningSpoilage = value; }
	}




}

    

[Serializable()]
public class StockQueueItem
{

	private long _StockID;
	private string _StockName;
	private bool _IsQuestion;
	private string _QuestionPhrase;

	private int _CostCentreID;
	private double _StockQuantity1;
	private double _StockQuantity2;
	private double _StockQuantity3;
	private double _StockQuantity4;

	private double _StockQuantity5;

	private double _StockPrice;
	public long StockID {
		get { return this._StockID; }
		set { this._StockID = value; }
	}

	public string StockName {
		get { return this._StockName; }
		set { this._StockName = value; }
	}

	public bool IsQuestion {
		get { return this._IsQuestion; }
		set { this._IsQuestion = value; }
	}

	public string QuestionPhrase {
		get { return this._QuestionPhrase; }
		set { this._QuestionPhrase = value; }
	}

	public int CostCentreID {
		get { return this._CostCentreID; }
		set { this._CostCentreID = value; }
	}

	public double StockQuantity1 {
		get { return this._StockQuantity1; }
		set { this._StockQuantity1 = value; }
	}
	public double StockQuantity2 {
		get { return this._StockQuantity2; }
		set { this._StockQuantity2 = value; }
	}

	public double StockQuantity3 {
		get { return this._StockQuantity3; }
		set { this._StockQuantity3 = value; }
	}

	public double StockQuantity4 {
		get { return this._StockQuantity4; }
		set { this._StockQuantity4 = value; }
	}

	public double StockQuantity5 {
		get { return this._StockQuantity5; }
		set { this._StockQuantity5 = value; }
	}

	public double StockPrice {
		get { return this._StockPrice; }
		set { this._StockPrice = value; }
	}


	/// <summary>
	/// Default constructor
	/// </summary>

    public StockQueueItem()
	{
	}

	public StockQueueItem(long StockID, string StockName, bool IsQuestion, string QuestionPhrase, int CostCentreID)
	{
		this._StockID = StockID;
		this._StockName = StockName;
		this._IsQuestion = IsQuestion;
		this._QuestionPhrase = QuestionPhrase;
		this._CostCentreID = CostCentreID;
	}


}


    

[Serializable()]
public class InputQueueItem
{

	private string _ID;
	private string _Name;
	private int _ItemType;
	private int _ItemInputType;
	private string _VisualQuestion;
	//Private _Answer As String
	private string _Value;

	private int _CostCentreID;
	private double _Qty1Answer;
	private double _Qty2Answer;
	private double _Qty3Answer;
	private double _Qty4Answer;

	private double _Qty5Answer;

	/// <summary>
	/// Default constructor
	/// </summary>

	public InputQueueItem()
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
	public InputQueueItem(string ID, string Name, int CostCentreID, int ItemType, int ItemInputType, string VisualQuestion, string Value, double Qty1Answer, double Qty2Answer = 0, double Qty3Answer = 0,
	double Qty4Answer = 0, double Qty5Answer = 0)
	{
		this._ID = ID;
		this._Name = Name;
		this._ItemType = ItemType;
		this._ItemInputType = ItemInputType;
		this._VisualQuestion = VisualQuestion;

		this._Value = Value;
		this._CostCentreID = CostCentreID;
		this._Qty1Answer = Qty1Answer;
		this._Qty2Answer = Qty2Answer;
		this._Qty3Answer = Qty3Answer;
		this._Qty4Answer = Qty4Answer;
		this._Qty5Answer = Qty5Answer;
	}

	public string ID {
		get { return this._ID; }
		set { this._ID = value; }
	}

	public string Name {
		get { return this._Name; }
		set { this._Name = value; }
	}

	public int ItemType {
		get { return this._ItemType; }
		set { this._ItemType = value; }
	}

	public int ItemInputType {
		get { return _ItemInputType; }
		set { _ItemInputType = value; }
	}

	public string VisualQuestion {
		get { return this._VisualQuestion; }
		set { this._VisualQuestion = value; }
	}


	public string Value {
		get { return this._Value; }
		set { this._Value = value; }
	}

	public int CostCentreID {
		get { return this._CostCentreID; }
		set { this._CostCentreID = value; }
	}

	public double Qty1Answer {
		get { return this._Qty1Answer; }
		set { this._Qty1Answer = value; }
	}

	public double Qty2Answer {
		get { return this._Qty2Answer; }
		set { this._Qty2Answer = value; }
	}

	public double Qty3Answer {
		get { return this._Qty3Answer; }
		set { this._Qty3Answer = value; }
	}

	public double Qty4Answer {
		get { return this._Qty4Answer; }
		set { this._Qty4Answer = value; }
	}

	public double Qty5Answer {
		get { return this._Qty5Answer; }
		set { this._Qty5Answer = value; }
	}



}



	

}

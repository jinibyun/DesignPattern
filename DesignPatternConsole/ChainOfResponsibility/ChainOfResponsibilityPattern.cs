using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternConsole.ChainOfResponsibility
{
    // Loan event argument holds Loan info
    public class LoanEventArgs : EventArgs
    {
        internal Loan Loan { get; set; }
    }

    /// <summary>
    /// The 'Handler' abstract class
    /// </summary>
    public abstract class Approver
    {
        // Loan event 
        public EventHandler<LoanEventArgs> Loan;

        // Loan event handler
        public abstract void LoanHandler(object sender, LoanEventArgs e);

        // Constructor
        public Approver()
        {
            Loan += LoanHandler;
        }

        public void ProcessRequest(Loan loan)
        {
            OnLoan(new LoanEventArgs { Loan = loan });
        }

        // Invoke the Loan event
        public virtual void OnLoan(LoanEventArgs e)
        {
            if (Loan != null)
            {
                Loan(this, e);
            }
        }

        // Sets or gets the next approver
        public Approver Successor { get; set; }
    }

    /// <summary>
    /// The 'ConcreteHandler' class
    /// </summary>
    public class Clerk : Approver
    {
        public override void LoanHandler(object sender, LoanEventArgs e)
        {
            if (e.Loan.Amount < 25000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                this.GetType().Name, e.Loan.Number);
            }
            else if (Successor != null)
            {
                Successor.LoanHandler(this, e);
            }
        }
    }

    /// <summary>
    /// The 'ConcreteHandler' class
    /// </summary>
    public class AssistantManager : Approver
    {
        public override void LoanHandler(object sender, LoanEventArgs e)
        {
            if (e.Loan.Amount < 45000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                this.GetType().Name, e.Loan.Number);
            }
            else if (Successor != null)
            {
                Successor.LoanHandler(this, e);
            }
        }
    }

    /// <summary>
    /// The 'ConcreteHandler' clas
    /// </summary>
    public class Manager : Approver
    {
        public override void LoanHandler(object sender, LoanEventArgs e)
        {
            if (e.Loan.Amount < 100000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                sender.GetType().Name, e.Loan.Number);
            }
            else if (Successor != null)
            {
                Successor.LoanHandler(this, e);
            }
            else
            {
                Console.WriteLine(
                "Request# {0} requires an executive meeting!",
                e.Loan.Number);
            }
        }
    }

    /// <summary>
    /// Class that holds request details
    /// </summary>
    public class Loan
    {
        public double Amount { get; set; }
        public string Purpose { get; set; }
        public int Number { get; set; }
    }
}

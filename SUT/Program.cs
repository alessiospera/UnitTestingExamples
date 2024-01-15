namespace SUT {
    public class Customer {
        private readonly DateTime _signUpTime;
        private int _orderNumber;

        //builder to set SignUpTime
        public Customer(DateTime signUpTime, int orderNumber = 0)
        {
            _signUpTime = signUpTime;
            OrderNumber = orderNumber;
        }

        public DateTime SignUpTime => _signUpTime; //sometimes "set" must be present because human are not perfect and could be useful maybe

        //when is needed move the data or to correct some errors, but in this case must be restricted,
        //because "SignUpTime" must be a constant value and not editable easily.
        //in this example i will not use it.
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentException"> set on negative values throws an exception </exception>


        public int OrderNumber
        {
            get => _orderNumber;
            set => _orderNumber = (value<0)?throw new ArgumentOutOfRangeException() : value;
        } //set must not exist, must be calculated

        /// <summary>
        /// Precalculate the price of an order
        /// Determined by the price of the item purchased 
        /// if the price is more than 100.00: discount of 3%
        /// if the user had did it at least 10 orders: discount of 0.5%
        /// if the user has been a user fo at least 5 years: discount of 1%
        /// </summary>
        /// <returns></returns>
        public double ExpectedCost(double itemCost)
        {
            var discount = 0.0;
            var result = itemCost;
            if (itemCost > 100) discount += 3;
            if (OrderNumber >= 10) discount += .5;
            
            return itemCost*(100-discount)/100;

        }
    }
}

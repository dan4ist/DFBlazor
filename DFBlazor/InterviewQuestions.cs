namespace DFBlazor {
    public class InterviewQuestions {

        public int[] TwoSum(int[] arr, int target) {
            Dictionary<int, int> indices = new Dictionary<int, int>();
            for ( int i = 0; i < arr.Length; i++ ) {
                int num = arr[i];
                if ( indices.ContainsKey(target - num) ) {
                    return new int[] { indices[target - num], i };
                }
                indices[num] = i;
            }
            return new int[0];
        }

        public int MaxProfit(int[] prices) {
            int minPrice = int.MaxValue;
            int maxProfit = 0;
            for ( int i = 0; i < prices.Length; i++ ) {
                int price = prices[i];
                minPrice = Math.Min(minPrice, price);
                maxProfit = Math.Max(maxProfit, price - minPrice);
            }
            return maxProfit;
        }






    }
}

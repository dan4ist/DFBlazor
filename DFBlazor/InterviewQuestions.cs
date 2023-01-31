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

        public int KadaneMaxSum(int[] arr) {
            int maxSum = 0;
            int currentSum = 0;

            for (int i = 0; i < arr.Length; i++) {
                currentSum = currentSum + arr[i];
                
                if (currentSum < 0) {
                    currentSum = 0;
				}

                if (maxSum < currentSum) { 
                    maxSum = currentSum;
                }
            }

            return maxSum;
        }

        public int MaxProduct(int[] arr) {
            int maxHere = arr[0];
            int minHere = arr[0];
            int maxSoFar = arr[0];

            for (int i = 1; i < arr.Length; i++) {
                int temp = Math.Max(Math.Max(arr[i], arr[i] * maxHere), arr[i] * minHere);
                minHere = Math.Min(Math.Min(arr[i], arr[i] * maxHere), arr[i] * minHere);
                maxHere = temp;
                maxSoFar = Math.Max(maxSoFar, maxHere);
            }

            return maxSoFar;
        }

        public int MinRotatedSortedArray(int[] arr) { 
            
        }
    }
}

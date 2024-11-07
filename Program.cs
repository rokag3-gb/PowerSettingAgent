namespace PowerSettingAgent;

public class Program
{
    static void Main(string[] args)
    {
        //Console.WriteLine("Hello, World!");

        try
        {
            // 현재 설정 확인
            Console.WriteLine($"현재 설정: {DisplayPowerSettings.GetDisplayTimeoutString()}");

            // "해당 없음"으로 설정
            //Console.WriteLine("\n'해당 없음'으로 설정 변경...");

            DisplayPowerSettings.SetDisplayTimeout(-1);

            // 변경된 설정 확인
            //Console.WriteLine($"변경된 설정: {DisplayPowerSettings.GetDisplayTimeoutString()}");

            //Console.WriteLine($"현재 설정: {DisplayPowerSettings.GetDisplayTimeoutString()}");

            // 다시 10분으로 설정
            //Console.WriteLine("\n10분으로 설정 변경...");
            
            //DisplayPowerSettings.SetDisplayTimeout(10);

            // 최종 설정 확인
            Console.WriteLine($"최종 설정: {DisplayPowerSettings.GetDisplayTimeoutString()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"오류 발생: {ex.Message}");
        }
    }
}
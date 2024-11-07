namespace PowerSettingAgent;

public class Program
{
    static void Main(string[] args)
    {
        //Console.WriteLine("Hello, World!");

        try
        {
            // 현재 설정 확인
            Console.WriteLine("현재 설정:");
            
            Console.WriteLine(PowerDisplaySettings.GetDisplayTimeoutString(PowerDisplaySettings.PowerType.AC));
            Console.WriteLine(PowerDisplaySettings.GetDisplayTimeoutString(PowerDisplaySettings.PowerType.DC));

            // 전원 사용 시 설정 변경
            Console.WriteLine("\n전원 사용 시 디스플레이 끄기 시간을 20분으로 변경...");
            PowerDisplaySettings.SetDisplayTimeout(20, PowerDisplaySettings.PowerType.AC);

            // 배터리 사용 시 설정 변경
            Console.WriteLine("배터리 사용 시 디스플레이 끄기 시간을 5분으로 변경...");
            PowerDisplaySettings.SetDisplayTimeout(5, PowerDisplaySettings.PowerType.DC);

            // 변경된 설정 확인
            Console.WriteLine("\n변경된 설정:");
            
            Console.WriteLine(PowerDisplaySettings.GetDisplayTimeoutString(PowerDisplaySettings.PowerType.AC));
            Console.WriteLine(PowerDisplaySettings.GetDisplayTimeoutString(PowerDisplaySettings.PowerType.DC));

            // "해당 없음" 설정 예시
            Console.WriteLine("\n전원 사용 시 '해당 없음'으로 설정...");
            
            PowerDisplaySettings.SetDisplayTimeout(-1, PowerDisplaySettings.PowerType.AC);

            // 최종 설정 확인
            Console.WriteLine("\n최종 설정:");

            Console.WriteLine(PowerDisplaySettings.GetDisplayTimeoutString(PowerDisplaySettings.PowerType.AC));
            Console.WriteLine(PowerDisplaySettings.GetDisplayTimeoutString(PowerDisplaySettings.PowerType.DC));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"오류 발생: {ex.Message}");
        }
    }
}
namespace PowerSettingAgent;

public class Program
{
    static void Main(string[] args)
    {
        try
        {
            // 현재 설정 확인
            Console.WriteLine("# 현재 설정");

            Console.WriteLine(PowerDisplaySettings.GetDisplayTimeoutString(PowerDisplaySettings.PowerType.DC));
            Console.WriteLine(PowerDisplaySettings.GetDisplayTimeoutString(PowerDisplaySettings.PowerType.AC));

            // 배터리 사용 시 설정 변경
            Console.WriteLine("\n배터리 사용 시 디스플레이 끄기 시간을 [20분]으로 변경...");
            PowerDisplaySettings.SetDisplayTimeout(20, PowerDisplaySettings.PowerType.DC);

            // 전원 사용 시 설정 변경
            Console.WriteLine("전원 사용 시 디스플레이 끄기 시간을 [해당 없음]으로 변경...");
            PowerDisplaySettings.SetDisplayTimeout(-1, PowerDisplaySettings.PowerType.AC);

            // 변경된 설정 확인
            Console.WriteLine("\n# 변경된 설정");

            Console.WriteLine(PowerDisplaySettings.GetDisplayTimeoutString(PowerDisplaySettings.PowerType.DC));
            Console.WriteLine(PowerDisplaySettings.GetDisplayTimeoutString(PowerDisplaySettings.PowerType.AC));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"오류 발생: {ex.Message}");
        }
    }
}
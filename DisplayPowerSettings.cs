namespace PowerSettingAgent;

using System.Runtime.InteropServices;

public class DisplayPowerSettings
{
    // Windows API 함수 선언
    [DllImport("powrprof.dll")]
    private static extern uint PowerGetActiveScheme(IntPtr UserRootPowerKey, out IntPtr ActivePolicyGuid);

    [DllImport("powrprof.dll")]
    private static extern uint PowerWriteACValueIndex(IntPtr RootPowerKey, ref Guid SchemeGuid,
        ref Guid SubGroupOfPowerSettingsGuid, ref Guid PowerSettingGuid, uint AcValueIndex);

    [DllImport("powrprof.dll")]
    private static extern uint PowerReadACValueIndex(IntPtr RootPowerKey, ref Guid SchemeGuid,
        ref Guid SubGroupOfPowerSettingsGuid, ref Guid PowerSettingGuid, out uint AcValueIndex);

    // 디스플레이 설정 관련 GUID
    private static Guid GUID_VIDEO_SUBGROUP = new Guid("7516b95f-f776-4464-8c53-06167f40cc99");
    private static Guid GUID_VIDEO_POWERDOWN_TIMEOUT = new Guid("3c0bc021-c8a8-4e07-a973-6b14cbcb2b7e");

    // 특별한 타임아웃 값
    public const uint NEVER_TIMEOUT = 0xFFFFFFFF;  // "해당 없음" 설정값

    /// <summary>
    /// 현재 디스플레이 끄기 설정을 가져옵니다.
    /// </summary>
    /// <returns>
    /// 음수: "해당 없음" 설정
    /// 0 이상: 설정된 시간(분)
    /// </returns>
    public static int GetDisplayTimeout()
    {
        try
        {
            uint result = PowerGetActiveScheme(IntPtr.Zero, out IntPtr activeSchemeGuid);
            if (result != 0)
            {
                throw new Exception($"전원 구성표를 가져오는데 실패했습니다. 에러 코드: {result}");
            }

            Guid activeScheme = Marshal.PtrToStructure<Guid>(activeSchemeGuid);

            result = PowerReadACValueIndex(
                IntPtr.Zero,
                ref activeScheme,
                ref GUID_VIDEO_SUBGROUP,
                ref GUID_VIDEO_POWERDOWN_TIMEOUT,
                out uint timeoutSeconds
            );

            Marshal.FreeHGlobal(activeSchemeGuid);

            if (result != 0)
            {
                throw new Exception($"설정값을 읽는데 실패했습니다. 에러 코드: {result}");
            }

            // "해당 없음" 설정 확인
            if (timeoutSeconds == NEVER_TIMEOUT)
            {
                return -1;  // "해당 없음"을 나타내는 특별한 값
            }

            return (int)(timeoutSeconds / 60);
        }
        catch (Exception ex)
        {
            throw new Exception($"디스플레이 끄기 시간을 가져오는데 실패했습니다: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// 디스플레이 끄기 시간을 설정합니다.
    /// </summary>
    /// <param name="minutes">
    /// -1: "해당 없음" 설정
    /// 0 이상: 설정할 시간(분)
    /// </param>
    public static void SetDisplayTimeout(int minutes)
    {
        if (minutes < -1)
        {
            throw new ArgumentException("유효하지 않은 시간값입니다. -1(해당 없음) 또는 0 이상의 값을 입력하세요.");
        }

        try
        {
            uint result = PowerGetActiveScheme(IntPtr.Zero, out IntPtr activeSchemeGuid);
            if (result != 0)
            {
                throw new Exception($"전원 구성표를 가져오는데 실패했습니다. 에러 코드: {result}");
            }

            Guid activeScheme = Marshal.PtrToStructure<Guid>(activeSchemeGuid);

            // 설정할 값 결정
            uint timeoutValue = (minutes == -1) ? NEVER_TIMEOUT : (uint)(minutes * 60);

            result = PowerWriteACValueIndex(
                IntPtr.Zero,
                ref activeScheme,
                ref GUID_VIDEO_SUBGROUP,
                ref GUID_VIDEO_POWERDOWN_TIMEOUT,
                timeoutValue
            );

            Marshal.FreeHGlobal(activeSchemeGuid);

            if (result != 0)
            {
                throw new Exception($"설정값을 변경하는데 실패했습니다. 에러 코드: {result}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"디스플레이 끄기 시간을 설정하는데 실패했습니다: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// 현재 설정을 문자열로 반환합니다.
    /// </summary>
    public static string GetDisplayTimeoutString()
    {
        int timeout = GetDisplayTimeout();
        return timeout == -1 ? "해당 없음" : $"{timeout}분";
    }
}
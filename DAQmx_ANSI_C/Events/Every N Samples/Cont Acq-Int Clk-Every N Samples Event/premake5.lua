project "Event-Every-N-Samples"
    kind "ConsoleApp"
    language "C"
    cdialect "C17"

    targetdir ("../../../bin/" .. outputdir .. "/%{prj.name}")
    objdir ("../../../bin-int/" .. outputdir .. "/%{prj.name}")
    
    files
    {
        "**.h",
        "**.c",
    }

    includedirs
    {
        "%{IncludeDir.glm}",
        "%{IncludeDir.NI}",
        "%{wks.location}/NMROSCore/src",
        "%{wks.location}/NMROSCore/vendor/spdlog/include",
    }
    libdirs
    {
        "%{wks.location}/NMROSCore/vendor/NI/lib64/msvc"
    }

    links
    {
        "NIDAQmx.lib",
        "NMROSCore"
    }
    
    filter "system:windows"
        systemversion "latest"
        
    filter "configurations:Debug"
        runtime "Debug"
        symbols "on"
        staticruntime "off"

    filter "configurations:Release"
        runtime "Release"
        optimize "on"
        staticruntime "on"

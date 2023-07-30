/*********************************************************************
*
* ANSI C Example program:
*    MultVoltUpdates-IntClk-DigStart.c
*
* Example Category:
*    AO
*
* Description:
*    This example demonstrates how to output a finite number of
*    voltage samples to an Analog Output Channel using an internal
*    sample clock and a digital start trigger.
*
* Instructions for Running:
*    1. Select the Physical Channel to correspond to where your
*       signal is output on the DAQ device.
*    2. Enter the Minimum and Maximum Voltage Ranges.
*    3. Select the Sample Clock source.
*    4. Select the Digital Trigger Source.
*    5. Specify the desired Trigger Edge.
*    6. Select the desired waveform type.
*    7. The rest of the parameters in the Waveform Information
*       section will affect the way the waveform is created, before
*       it's sent to the analog output of the board. Select the
*       number of samples per cycle and the total number of cycles to
*       be used as waveform data.
*    8. Select the desired waveform frequency; this will determine
*       the update rate of the operation.
*
* Steps:
*    1. Create a task.
*    2. Create an Analog Output Voltage Channel.
*    3. Setup the Timing for the Measurement. In this example we use
*       the internal DAQ Device's clock to produce a finite number of
*       samples.
*    4. Define the Triggering parameters: Source and Edge.
*    5. Use the Write function to Generate Multiple Samples to 1
*       Channel on the Data Acquisition Card. The auto start
*       parameter is set to False, so the Start function must be
*       explicitly called to begin the Voltage Generation.
*    6. Call the Start function.
*    7. Call the Clear Task function to clear the Task.
*    8. Display an error if any.
*
* I/O Connections Overview:
*    Make sure your signal output terminal matches the Physical
*    Channel I/O Control. Also, make sure your digital trigger
*    terminal matches the Trigger Source Control. For further
*    connection information, refer to your hardware reference manual.
*
*********************************************************************/

/*********************************************************************
* Microsoft Windows Vista User Account Control
* Running certain applications on Microsoft Windows Vista requires
* administrator privileges, because the application name contains keywords
* such as setup, update, or install. To avoid this problem, you must add an
* additional manifest to the application that specifies the privileges
* required to run the application. Some ANSI-C NI-DAQmx examples include
* these keywords. Therefore, these examples are shipped with an additional
* manifest file that you must embed in the example executable. The manifest
* file is named [ExampleName].exe.manifest, where [ExampleName] is the
* NI-provided example name. For information on how to embed the manifest
* file, refer to http://msdn2.microsoft.com/en-us/library/bb756929.aspx.
*********************************************************************/

#include <stdio.h>
#include <math.h>
#include <NIDAQmx.h>

#define DAQmxErrChk(functionCall) if( DAQmxFailed(error=(functionCall)) ) goto Error; else

int main(void)
{
	int         error=0;
	TaskHandle  taskHandle=0;
	float64     data[4000];
	char        errBuff[2048]={'\0'};
	int			i=0;
	int32   	written;

	for(;i<4000;i++)
		data[i] = 5.0*(double)i/4000.0;

	/*********************************************/
	// DAQmx Configure Code
	/*********************************************/
	DAQmxErrChk (DAQmxCreateTask("",&taskHandle));
	DAQmxErrChk (DAQmxCreateAOVoltageChan(taskHandle,"Dev1/ao0","",-10.0,10.0,DAQmx_Val_Volts,NULL));
	DAQmxErrChk (DAQmxCfgSampClkTiming(taskHandle,"",1000.0,DAQmx_Val_Rising,DAQmx_Val_FiniteSamps,4000));
	DAQmxErrChk (DAQmxCfgDigEdgeStartTrig(taskHandle,"/Dev0/PFI0",DAQmx_Val_Rising));

	/*********************************************/
	// DAQmx Write Code
	/*********************************************/
	DAQmxErrChk (DAQmxWriteAnalogF64(taskHandle,4000,0,10.0,DAQmx_Val_GroupByChannel,data,&written,NULL));

	/*********************************************/
	// DAQmx Start Code
	/*********************************************/
	DAQmxErrChk (DAQmxStartTask(taskHandle));

	/*********************************************/
	// DAQmx Wait Code
	/*********************************************/
	DAQmxErrChk (DAQmxWaitUntilTaskDone(taskHandle,10.0));

Error:
	if( DAQmxFailed(error) )
		DAQmxGetExtendedErrorInfo(errBuff,2048);
	if( taskHandle!=0 ) {
		/*********************************************/
		// DAQmx Stop Code
		/*********************************************/
		DAQmxStopTask(taskHandle);
		DAQmxClearTask(taskHandle);
	}
	if( DAQmxFailed(error) )
		printf("DAQmx Error: %s\n",errBuff);
	printf("End of program, press Enter key to quit\n");
	getchar();
	return 0;
}

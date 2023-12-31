'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'
' Example program:
'   AcqVoltageSamples_IntClk
'
' Category:
'   AI
'
' Description:
'   This example demonstrates how to acquire a finite amount of data using an
'   internal clock.
'
' Instructions for running:
'   1.  Select the physical channel corresponding to the input signal on the DAQ
'       device.
'   2.  Enter the minimum and maximum voltage ranges.Note:  For better accuracy,
'       try to match the input ranges to the expected voltage level of the
'       measured signal.
'   3.  Select the number of samples per channel to acquire.
'   4.  Set the rate in Hz for the internal clock.Note:  The rate should be at
'       least twice as fast as the maximum frequency component of the signal
'       being acquired.
'
' Steps:
'   1.  Create a new task and an analog input voltage channel.
'   2.  Configure the task to use the internal clock.
'   3.  Create a AnalogMultiChannelReader and associate it with the task by
'       using the task's stream.
'   4.  Call AnalogMultiChannelReader.ReadWaveform to acquire and display the
'       data.
'   5.  Dispose the Task object to clean-up any resources associated with the
'       task.
'   6.  Handle any DaqExceptions, if they occur.
'
' I/O Connections Overview:
'   Make sure your signal input terminals match the physical channel text box. 
'   For more information on the input and output terminals for your device, open
'   the NI-DAQmx Help, and refer to the NI-DAQmx Device Terminals and Device
'   Considerations books in the table of contents.
'
' Microsoft Windows Vista User Account Control
'   Running certain applications on Microsoft Windows Vista requires
'   administrator privileges, 
'   because the application name contains keywords such as setup, update, or
'   install. To avoid this problem, 
'   you must add an additional manifest to the application that specifies the
'   privileges required to run 
'   the application. Some Measurement Studio NI-DAQmx examples for Visual Studio
'   include these keywords. 
'   Therefore, all examples for Visual Studio are shipped with an additional
'   manifest file that you must 
'   embed in the example executable. The manifest file is named
'   [ExampleName].exe.manifest, where [ExampleName] 
'   is the NI-provided example name. For information on how to embed the manifest
'   file, refer to http://msdn2.microsoft.com/en-us/library/bb756929.aspx.Note: 
'   The manifest file is not provided with examples for Visual Studio .NET 2003.
'
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports NationalInstruments.DAQmx

Public Class MainForm
    Inherits System.Windows.Forms.Form


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        Application.EnableVisualStyles()
        Application.DoEvents()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        physicalChannelComboBox.Items.AddRange(DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.AI, PhysicalChannelAccess.External))
        If (physicalChannelComboBox.Items.Count > 0) Then
            physicalChannelComboBox.SelectedIndex = 0
        End If

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents startButton As System.Windows.Forms.Button
    Friend WithEvents rateLabel As System.Windows.Forms.Label
    Friend WithEvents samplesLabel As System.Windows.Forms.Label
    Friend WithEvents channelParametersGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents maximumLabel As System.Windows.Forms.Label
    Friend WithEvents minimumLabel As System.Windows.Forms.Label
    Friend WithEvents physicalChannelLabel As System.Windows.Forms.Label
    Friend WithEvents timingParametersGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents samplesPerChanNumeric As System.Windows.Forms.NumericUpDown
    Friend WithEvents acquisitionResultGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents acquisitionDataGrid As System.Windows.Forms.DataGrid
    Friend WithEvents rateNumeric As System.Windows.Forms.NumericUpDown
    Friend WithEvents minimumValueNumeric As System.Windows.Forms.NumericUpDown
    Friend WithEvents maximumValueNumeric As System.Windows.Forms.NumericUpDown
    Friend WithEvents physicalChannelComboBox As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(MainForm))
        Me.startButton = New System.Windows.Forms.Button
        Me.timingParametersGroupBox = New System.Windows.Forms.GroupBox
        Me.samplesLabel = New System.Windows.Forms.Label
        Me.samplesPerChanNumeric = New System.Windows.Forms.NumericUpDown
        Me.rateNumeric = New System.Windows.Forms.NumericUpDown
        Me.rateLabel = New System.Windows.Forms.Label
        Me.channelParametersGroupBox = New System.Windows.Forms.GroupBox
        Me.physicalChannelComboBox = New System.Windows.Forms.ComboBox
        Me.minimumValueNumeric = New System.Windows.Forms.NumericUpDown
        Me.maximumValueNumeric = New System.Windows.Forms.NumericUpDown
        Me.maximumLabel = New System.Windows.Forms.Label
        Me.minimumLabel = New System.Windows.Forms.Label
        Me.physicalChannelLabel = New System.Windows.Forms.Label
        Me.acquisitionResultGroupBox = New System.Windows.Forms.GroupBox
        Me.acquisitionDataGrid = New System.Windows.Forms.DataGrid
        Me.timingParametersGroupBox.SuspendLayout()
        CType(Me.samplesPerChanNumeric, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rateNumeric, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.channelParametersGroupBox.SuspendLayout()
        CType(Me.minimumValueNumeric, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.maximumValueNumeric, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.acquisitionResultGroupBox.SuspendLayout()
        CType(Me.acquisitionDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'startButton
        '
        Me.startButton.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.startButton.Location = New System.Drawing.Point(80, 256)
        Me.startButton.Name = "startButton"
        Me.startButton.Size = New System.Drawing.Size(80, 24)
        Me.startButton.TabIndex = 2
        Me.startButton.Text = "Start"
        '
        'timingParametersGroupBox
        '
        Me.timingParametersGroupBox.Controls.Add(Me.samplesLabel)
        Me.timingParametersGroupBox.Controls.Add(Me.samplesPerChanNumeric)
        Me.timingParametersGroupBox.Controls.Add(Me.rateNumeric)
        Me.timingParametersGroupBox.Controls.Add(Me.rateLabel)
        Me.timingParametersGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.timingParametersGroupBox.Location = New System.Drawing.Point(8, 144)
        Me.timingParametersGroupBox.Name = "timingParametersGroupBox"
        Me.timingParametersGroupBox.Size = New System.Drawing.Size(232, 88)
        Me.timingParametersGroupBox.TabIndex = 1
        Me.timingParametersGroupBox.TabStop = False
        Me.timingParametersGroupBox.Text = "Timing Parameters"
        '
        'samplesLabel
        '
        Me.samplesLabel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.samplesLabel.Location = New System.Drawing.Point(16, 24)
        Me.samplesLabel.Name = "samplesLabel"
        Me.samplesLabel.Size = New System.Drawing.Size(104, 16)
        Me.samplesLabel.TabIndex = 0
        Me.samplesLabel.Text = "Samples / Channel:"
        '
        'samplesPerChanNumeric
        '
        Me.samplesPerChanNumeric.Location = New System.Drawing.Point(120, 24)
        Me.samplesPerChanNumeric.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.samplesPerChanNumeric.Name = "samplesPerChanNumeric"
        Me.samplesPerChanNumeric.Size = New System.Drawing.Size(96, 20)
        Me.samplesPerChanNumeric.TabIndex = 1
        Me.samplesPerChanNumeric.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        '
        'rateNumeric
        '
        Me.rateNumeric.DecimalPlaces = 2
        Me.rateNumeric.Location = New System.Drawing.Point(120, 56)
        Me.rateNumeric.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.rateNumeric.Name = "rateNumeric"
        Me.rateNumeric.Size = New System.Drawing.Size(96, 20)
        Me.rateNumeric.TabIndex = 3
        Me.rateNumeric.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        '
        'rateLabel
        '
        Me.rateLabel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.rateLabel.Location = New System.Drawing.Point(16, 56)
        Me.rateLabel.Name = "rateLabel"
        Me.rateLabel.Size = New System.Drawing.Size(64, 16)
        Me.rateLabel.TabIndex = 2
        Me.rateLabel.Text = "Rate (Hz):"
        '
        'channelParametersGroupBox
        '
        Me.channelParametersGroupBox.Controls.Add(Me.physicalChannelComboBox)
        Me.channelParametersGroupBox.Controls.Add(Me.minimumValueNumeric)
        Me.channelParametersGroupBox.Controls.Add(Me.maximumValueNumeric)
        Me.channelParametersGroupBox.Controls.Add(Me.maximumLabel)
        Me.channelParametersGroupBox.Controls.Add(Me.minimumLabel)
        Me.channelParametersGroupBox.Controls.Add(Me.physicalChannelLabel)
        Me.channelParametersGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.channelParametersGroupBox.Location = New System.Drawing.Point(8, 12)
        Me.channelParametersGroupBox.Name = "channelParametersGroupBox"
        Me.channelParametersGroupBox.Size = New System.Drawing.Size(232, 124)
        Me.channelParametersGroupBox.TabIndex = 0
        Me.channelParametersGroupBox.TabStop = False
        Me.channelParametersGroupBox.Text = "Channel Parameters"
        '
        'physicalChannelComboBox
        '
        Me.physicalChannelComboBox.Location = New System.Drawing.Point(120, 24)
        Me.physicalChannelComboBox.Name = "physicalChannelComboBox"
        Me.physicalChannelComboBox.Size = New System.Drawing.Size(96, 21)
        Me.physicalChannelComboBox.TabIndex = 1
        Me.physicalChannelComboBox.Text = "Dev1/ai0"
        '
        'minimumValueNumeric
        '
        Me.minimumValueNumeric.DecimalPlaces = 2
        Me.minimumValueNumeric.Location = New System.Drawing.Point(120, 56)
        Me.minimumValueNumeric.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.minimumValueNumeric.Minimum = New Decimal(New Integer() {10, 0, 0, -2147483648})
        Me.minimumValueNumeric.Name = "minimumValueNumeric"
        Me.minimumValueNumeric.Size = New System.Drawing.Size(96, 20)
        Me.minimumValueNumeric.TabIndex = 3
        Me.minimumValueNumeric.Value = New Decimal(New Integer() {100, 0, 0, -2147418112})
        '
        'maximumValueNumeric
        '
        Me.maximumValueNumeric.DecimalPlaces = 2
        Me.maximumValueNumeric.Location = New System.Drawing.Point(120, 88)
        Me.maximumValueNumeric.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.maximumValueNumeric.Minimum = New Decimal(New Integer() {10, 0, 0, -2147483648})
        Me.maximumValueNumeric.Name = "maximumValueNumeric"
        Me.maximumValueNumeric.Size = New System.Drawing.Size(96, 20)
        Me.maximumValueNumeric.TabIndex = 5
        Me.maximumValueNumeric.Value = New Decimal(New Integer() {100, 0, 0, 65536})
        '
        'maximumLabel
        '
        Me.maximumLabel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.maximumLabel.Location = New System.Drawing.Point(16, 88)
        Me.maximumLabel.Name = "maximumLabel"
        Me.maximumLabel.Size = New System.Drawing.Size(96, 16)
        Me.maximumLabel.TabIndex = 4
        Me.maximumLabel.Text = "Maximum (Volts):"
        '
        'minimumLabel
        '
        Me.minimumLabel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.minimumLabel.Location = New System.Drawing.Point(16, 56)
        Me.minimumLabel.Name = "minimumLabel"
        Me.minimumLabel.Size = New System.Drawing.Size(96, 16)
        Me.minimumLabel.TabIndex = 2
        Me.minimumLabel.Text = "Minimum (Volts):"
        '
        'physicalChannelLabel
        '
        Me.physicalChannelLabel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.physicalChannelLabel.Location = New System.Drawing.Point(16, 26)
        Me.physicalChannelLabel.Name = "physicalChannelLabel"
        Me.physicalChannelLabel.Size = New System.Drawing.Size(96, 16)
        Me.physicalChannelLabel.TabIndex = 0
        Me.physicalChannelLabel.Text = "Physical Channel:"
        '
        'acquisitionResultGroupBox
        '
        Me.acquisitionResultGroupBox.Controls.Add(Me.acquisitionDataGrid)
        Me.acquisitionResultGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.acquisitionResultGroupBox.Location = New System.Drawing.Point(248, 12)
        Me.acquisitionResultGroupBox.Name = "acquisitionResultGroupBox"
        Me.acquisitionResultGroupBox.Size = New System.Drawing.Size(272, 276)
        Me.acquisitionResultGroupBox.TabIndex = 3
        Me.acquisitionResultGroupBox.TabStop = False
        Me.acquisitionResultGroupBox.Text = "Acquisition Results"
        '
        'acquisitionDataGrid
        '
        Me.acquisitionDataGrid.AllowSorting = False
        Me.acquisitionDataGrid.DataMember = ""
        Me.acquisitionDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.acquisitionDataGrid.Location = New System.Drawing.Point(8, 24)
        Me.acquisitionDataGrid.Name = "acquisitionDataGrid"
        Me.acquisitionDataGrid.ParentRowsVisible = False
        Me.acquisitionDataGrid.ReadOnly = True
        Me.acquisitionDataGrid.Size = New System.Drawing.Size(256, 248)
        Me.acquisitionDataGrid.TabIndex = 1
        Me.acquisitionDataGrid.TabStop = False
        '
        'MainForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(522, 296)
        Me.Controls.Add(Me.acquisitionResultGroupBox)
        Me.Controls.Add(Me.startButton)
        Me.Controls.Add(Me.timingParametersGroupBox)
        Me.Controls.Add(Me.channelParametersGroupBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Acquire Voltage Samples - Internal Clock"
        Me.timingParametersGroupBox.ResumeLayout(False)
        CType(Me.samplesPerChanNumeric, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rateNumeric, System.ComponentModel.ISupportInitialize).EndInit()
        Me.channelParametersGroupBox.ResumeLayout(False)
        CType(Me.minimumValueNumeric, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.maximumValueNumeric, System.ComponentModel.ISupportInitialize).EndInit()
        Me.acquisitionResultGroupBox.ResumeLayout(False)
        CType(Me.acquisitionDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private myTask As Task  'A new task is created when the start button is pressed
    Private reader As AnalogMultiChannelReader

    Private dataColumn As DataColumn()             'Channels of Data
    Private dataTable As DataTable = New DataTable 'Table to Display Data


    Private Sub startButton_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startButton.Click
        startButton.Enabled = False

        Try
            ' Create a new task
            myTask = New Task()

            ' Initialize local variables
            Dim sampleRate As Double = Convert.ToDouble(rateNumeric.Value)
            Dim rangeMin As Double = Convert.ToDouble(minimumValueNumeric.Value)
            Dim rangeMax As Double = Convert.ToDouble(maximumValueNumeric.Value)
            Dim samplesPerChan As Int32 = Convert.ToInt32(samplesPerChanNumeric.Value)

            ' Create a channel
            myTask.AIChannels.CreateVoltageChannel(physicalChannelComboBox.Text, "", CType(-1, AITerminalConfiguration), rangeMin, rangeMax, AIVoltageUnits.Volts)

            ' Configure timing specs 
            myTask.Timing.ConfigureSampleClock("", sampleRate, SampleClockActiveEdge.Rising, SampleQuantityMode.FiniteSamples, samplesPerChan)

            ' Verify the task
            myTask.Control(TaskAction.Verify)
            reader = New AnalogMultiChannelReader(myTask.Stream)

            ' Prepare the table for data
            InitializeDataTable(myTask.AIChannels, dataTable)
            acquisitionDataGrid.DataSource = dataTable

            ' Read the data
            Dim data() As AnalogWaveform(Of Double) = reader.ReadWaveform(samplesPerChan)

            dataToDataTable(data, dataTable)
        Catch ex As DaqException
            MessageBox.Show(ex.Message)
        Finally
            myTask.Dispose()
            startButton.Enabled = True
        End Try
    End Sub

    Private Sub dataToDataTable(ByVal sourceArray As AnalogWaveform(Of Double)(), ByRef dataTable As DataTable)
        ' Iterate over channels
        Dim currentLineIndex As Integer = 0
        For Each waveform As AnalogWaveform(Of Double) In sourceArray
            Dim dataCount As Integer = 0
            If waveform.Samples.Count < 10 Then
                dataCount = waveform.Samples.Count
            Else
                dataCount = 10
            End If
            For sample As Integer = 0 To (dataCount - 1)
                dataTable.Rows(sample)(currentLineIndex) = waveform.Samples(sample).Value
            Next
            currentLineIndex += 1
        Next
    End Sub
	
    Public Sub InitializeDataTable(ByVal channelCollection As AIChannelCollection, ByRef data As dataTable)
        Dim numOfChannels As Int16 = channelCollection.Count
        data.Rows.Clear()
        data.Columns.Clear()
        dataColumn = New DataColumn(numOfChannels) {}
        Dim numOfRows As Int16 = 10
        Dim currentChannelIndex As Int16 = 0
        Dim currentDataIndex As Int16 = 0

        For currentChannelIndex = 0 To (numOfChannels - 1)
            dataColumn(currentChannelIndex) = New DataColumn
            dataColumn(currentChannelIndex).DataType = System.Type.GetType("System.Double")
            dataColumn(currentChannelIndex).ColumnName = channelCollection(currentChannelIndex).PhysicalName
        Next

        data.Columns.AddRange(dataColumn)

        For currentDataIndex = 0 To (numOfRows - 1)
            Dim rowArr As Object() = New Object(numOfChannels - 1) {}
            data.Rows.Add(rowArr)
        Next
    End Sub
End Class

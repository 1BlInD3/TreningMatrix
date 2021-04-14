# <img alt="" src="Resources/Icon/gradcap.png" height=32 style="float:left"/>&nbsp;TrainingMatrix
WPF application for employee training administration and lookup  
Written in C# and XAML

<div style="background-color:dimgray;display:inline-block">
   <img alt="" src="Resources/Snapshots/stats.png" width=49% style="display:inline-block;float:left;padding:0.5%"/>
   <img alt="" src="Resources/Snapshots/stations.png" width=49% style="display:inline-block;float:left;padding:0.5%"/>
   <img alt="" src="Resources/Snapshots/employees.png" width=49% style="display:inline-block;float:left;padding:0.5%"/>
   <img alt="" src="Resources/Snapshots/report.png" title="asd" width=49% style="display:inline-block;float:left;padding:0.5%"/>
</div>

## Main functions
- reading statistics
- adding / editing / removing employees and trainings
- see trained employees by production line and station
- see training by employee
- data import
- report creation
- printing documents

## Source code main directory structure
```
.
├───Commands                # classes implementing the ICommand interface
├───Converters              # value converters for views
├───Models                  # data models to wrap
├───packages                # external dependencies
├───Properties              # assembly information
├───Resources               # images and icon files
├───ViewModels              # wrappers for data models
├───Views                   # xaml
└───Windows                 # xaml
```

## External dependencies
**EntityFramework.6.2.0** - *for a database interface*  
**LiveCharts.0.9.7** - *as a prequisite for LiveCharts.Wpf*  
**LiveCharts.Wpf.0.9.7** - *for generating charts programmatically*  
**Microsoft.Office.Interop.Excel.15.0.4795.1000** - *for generating excel files*  

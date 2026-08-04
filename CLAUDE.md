# BrainWavesWPF
BrainWaves는 바이노럴 비트를 생성하는 WPF 데스크톱 애플리케이션입니다.

STACK: .NET 8, WPF, MVVM

Views/         : XAML UI (프레임 네비게이션)
ViewModels/    : 화면 로직·상태
Services/      : 오디오 합성·재생
Converters/    : 값 변환기
Behaviors/     : 첨부 속성으로 붙이는 UI 동작



양쪽 귀에 약간 다른 주파수의 오디오를 재생하여 뇌파 패턴에 영향을 줄 수 있는 비트를 생성합니다.
.NET 8.0으로 빌드되었으며 MVVM 아키텍처를 따라야합니다.

## 네비게이션 아키텍처
앱은 MainViewModel이 제어하는 프레임 기반 네비게이션을 사용합니다. 네비게이션 명령은 메인 윈도우의 네비게이션 바를 유지하면서 세 페이지 간을 전환합니다. 각 페이지는 중앙 프레임 요소에 로드됩니다.

### Project Features
- Material Design 기반의 현대적인 UI
- 13가지 사전 설정된 뇌파 상태 (집중, 수면, 명상 등)
- 실시간 주파수 조절 및 볼륨 컨트롤
- 파동 타입별 색상 구분 (Gamma, Beta, Alpha, Theta, Delta)

## Project Architecture
애플리케이션은 세 가지 주요 레이어로 구성된 MVVM 패턴을 따릅니다:

## Project Library Dependencies
- **CommunityToolkit.Mvvm (8.4.0)** - 소스 생성기 기반 MVVM 프레임워크, ObservableObject, RelayCommand, Messaging 제공
- **MaterialDesignThemes (5.2.2-ci998)** - Material Design 컴포넌트 및 스타일
- **MaterialDesignColors (5.2.2-ci998)** - Material Design 색상 팔레트


### Services (`/Services/`)
- `AudioService.cs` - 

### View (`/View/`)
- `Waves.xaml` - 
- `Presets.xaml` - 사전 구성된 주파수 조합 (각 파동 타입별 고유 색상)
- `Settings.xaml` - 애플리케이션 정보, 오픈소스 라이브러리, GitHub 링크

### ViewModel (`/ViewModel/`)
- `MainViewModel.cs` - 네비게이션 처리 및 프리셋 컬렉션 관리 (집중, 수면, 명상 등 13개의 사전 구성 상태)
- `WavesViewModel.cs` - 주파수 조절, 재생/정지, 볼륨 컨트롤 관리. WeakReferenceMessenger를 통한 프리셋 선택 수신
- `PresetsViewModel.cs` - 프리셋 목록 관리 및 선택 시 재생 토글 기능. PresetDataViewModel로 UI 상태 확장

### Converters (`/Converters/`)
- `BoolToPlayStopTextConverter.cs` - 재생 상태에 따른 텍스트 변환

### Behaviors (`/Behaviors/`)
- `WindowAutoFit.cs` - 창의 최소 크기와 시작 크기를 콘텐츠가 요구하는 크기에서 도출한다. 창에 `WindowAutoFit.IsEnabled`를 붙이면 페이지를 띄울 때마다 다시 측정한다. 목록처럼 항목 수만큼 길어지는 페이지는 `WindowAutoFit.FitsContent="False"`로 선언해 창이 그 길이를 따라가지 않게 한다.

## 창 크기 정책
창 크기를 사람이 고른 값으로 두면 폰트, DPI 배율, 요소 추가로 콘텐츠 요구 높이가 바뀔 때 조용히 어긋나 스크롤바가 생긴다. 그래서 `MainWindow`는 높이를 지정하지 않고 `WindowAutoFit`이 측정한 값을 쓴다. 각 페이지의 ScrollViewer는 지우지 않고 안전망으로 남긴다. 콘텐츠 요구가 화면 작업 영역을 넘어 창을 더 키울 수 없을 때 요소가 잘리지 않게 받아내는 역할이다.

새 페이지를 추가할 때는 선언 없이 두면 콘텐츠가 모두 보이도록 창이 맞춰진다. 항목 수에 따라 길어지는 페이지만 `FitsContent="False"`를 붙인다.

### 핵심 기능
- `PlaySound.cs` - System.Media.SoundPlayer를 사용하여 각 채널에 다른 사인파 주파수를 가진 스테레오 WAV 오디오 생성

## External Instructions
@.claude/build_and_release.md

### 컴포넌트 스타일
- **카드**: 20px 둥근 모서리, 내부 패딩 16-20px
- **버튼**: 투명 배경에 보라색 테두리 또는 보라색 채움
- **슬라이더**: 보라색 트랙
- **아이콘**: Material Design 아이콘 사용

## 메시징 패턴

앱은 CommunityToolkit.Mvvm의 WeakReferenceMessenger를 사용하여 컴포넌트 간 통신:

- `PresetSelectedMessage`: 프리셋 선택 시 주파수 데이터 전달
- `PlaybackStateChangedMessage`: 재생 상태 변경 알림
- `AudioParametersChangedMessage`: 주파수/볼륨 변경 알림

## GitHub 리포지토리

https://github.com/Sia819/BrainWavesWPF

## Project Tree
BrainWavesWPF
├─ .claude
│  ├─ build_and_release.md
│  ├─ compact_summary.md
│  └─ settings.local.json
├─ BrainWaves
│  ├─ BrainWaves
│  │  ├─ App.xaml
│  │  ├─ App.xaml.cs
│  │  ├─ AssemblyInfo.cs
│  │  ├─ Behaviors
│  │  │  └─ WindowAutoFit.cs
│  │  ├─ BrainWaves.csproj
│  │  ├─ Converters
│  │  │  └─ BoolToPlayStopTextConverter.cs
│  │  ├─ Model
│  │  │  └─ PresetData.cs
│  │  ├─ PlaySound.cs
│  │  ├─ Resources
│  │  │  ├─ Animations
│  │  │  │  └─ Storyboards.xaml
│  │  │  └─ Styles
│  │  │     ├─ ButtonStyles.xaml
│  │  │     ├─ CardStyles.xaml
│  │  │     ├─ SliderStyles.xaml
│  │  │     └─ TextStyles.xaml
│  │  ├─ Services
│  │  │  └─ AudioService.cs
│  │  ├─ View
│  │  │  ├─ MainWindow.xaml
│  │  │  ├─ MainWindow.xaml.cs
│  │  │  ├─ Presets.xaml
│  │  │  ├─ Presets.xaml.cs
│  │  │  ├─ Settings.xaml
│  │  │  ├─ Settings.xaml.cs
│  │  │  ├─ Waves.xaml
│  │  │  └─ Waves.xaml.cs
│  │  └─ ViewModel
│  │     ├─ MainViewModel.cs
│  │     ├─ PresetsViewModel.cs
│  │     └─ WavesViewModel.cs
│  └─ BrainWaves.sln
├─ CLAUDE.md
├─ Images
│  ├─ program_presets.png
│  ├─ program_settings.png
│  └─ program_waves.png
├─ LICENSE
└─ README.md

```
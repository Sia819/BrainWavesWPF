# CLAUDE.md

이 파일은 이 저장소의 코드 작업 시 Claude Code (claude.ai/code)에 대한 지침을 제공합니다.

## 프로젝트 개요

BrainWaves는 바이노럴 비트를 생성하는 WPF 데스크톱 애플리케이션입니다. 양쪽 귀에 약간 다른 주파수의 오디오를 재생하여 뇌파 패턴에 영향을 줄 수 있는 비트를 생성합니다. .NET 6.0으로 빌드되었으며 MVVM 아키텍처를 따릅니다.

## 개발 명령어

```bash
# 프로젝트 빌드
dotnet build

# 애플리케이션 실행
dotnet run --project BrainWaves/BrainWaves/BrainWaves.csproj

# 빌드 아티팩트 정리
dotnet clean

# NuGet 패키지 복원
dotnet restore

# 릴리스용 퍼블리시
dotnet publish -c Release
```

## 아키텍처

애플리케이션은 세 가지 주요 레이어로 구성된 MVVM 패턴을 따릅니다:

### Model (`/Model/`)
- `PresetData.cs` - 좌/우 주파수, 공명 계산, 파형 유형 분류(감마, 베타, 알파, 세타, 델타)를 포함한 뇌파 프리셋 정의

### View (`/View/`)
- `MainWindow.xaml` - 하단 네비게이션 바가 있는 프레임 기반 네비게이션 컨테이너
- `Waves.xaml` - 수동 주파수 제어 페이지
- `Presets.xaml` - 사전 구성된 주파수 조합
- `Settings.xaml` - 애플리케이션 정보 및 종속성

### ViewModel (`/ViewModel/`)
- `MainViewModel.cs` - 네비게이션 처리 및 프리셋 컬렉션 관리 (집중, 수면, 명상 등 13개의 사전 구성 상태)

### 핵심 기능
- `PlaySound.cs` - System.Media.SoundPlayer를 사용하여 각 채널에 다른 사인파 주파수를 가진 스테레오 WAV 오디오 생성

## 주요 종속성

- **CommunityToolkit.Mvvm** - MVVM을 위한 ICommand 구현 제공
- **PropertyChanged.Fody** - IL 위빙을 통해 INotifyPropertyChanged 자동 구현

## 네비게이션 아키텍처

앱은 MainViewModel이 제어하는 프레임 기반 네비게이션을 사용합니다. 네비게이션 명령은 메인 윈도우의 네비게이션 바를 유지하면서 세 페이지 간을 전환합니다. 각 페이지는 중앙 프레임 요소에 로드됩니다.
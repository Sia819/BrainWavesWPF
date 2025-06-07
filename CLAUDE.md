# CLAUDE.md

이 파일은 이 저장소의 코드 작업 시 Claude Code (claude.ai/code)에 대한 지침을 제공합니다.

## 프로젝트 개요

BrainWaves는 바이노럴 비트를 생성하는 WPF 데스크톱 애플리케이션입니다. 양쪽 귀에 약간 다른 주파수의 오디오를 재생하여 뇌파 패턴에 영향을 줄 수 있는 비트를 생성합니다. .NET 8.0으로 빌드되었으며 MVVM 아키텍처를 따릅니다.

### 주요 특징
- Material Design 기반의 현대적인 UI
- 짙은 남색(#0E0E1A) 배경의 다크 테마
- 13가지 사전 설정된 뇌파 상태 (집중, 수면, 명상 등)
- 실시간 주파수 조절 및 볼륨 컨트롤
- 파동 타입별 색상 구분 (Gamma, Beta, Alpha, Theta, Delta)

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

### Services (`/Services/`)
- `AudioService.cs` - 싱글톤 패턴으로 구현된 중앙 집중식 오디오 재생 관리자. 동시 재생 방지 및 디바운싱 로직 포함

### View (`/View/`)
- `MainWindow.xaml` - 하단 네비게이션 바가 있는 프레임 기반 네비게이션 컨테이너 (900x450 크기)
- `Waves.xaml` - 수동 주파수 제어 페이지 (남색 그라데이션 헤더, 보라색 주파수 표시)
- `Presets.xaml` - 사전 구성된 주파수 조합 (각 파동 타입별 고유 색상)
- `Settings.xaml` - 애플리케이션 정보, 오픈소스 라이브러리, GitHub 링크

### ViewModel (`/ViewModel/`)
- `MainViewModel.cs` - 네비게이션 처리 및 프리셋 컬렉션 관리 (집중, 수면, 명상 등 13개의 사전 구성 상태)
- `WavesViewModel.cs` - 주파수 조절, 재생/정지, 볼륨 컨트롤 관리. WeakReferenceMessenger를 통한 프리셋 선택 수신
- `PresetsViewModel.cs` - 프리셋 목록 관리 및 선택 시 재생 토글 기능. PresetDataViewModel로 UI 상태 확장

### Converters (`/Converters/`)
- `BoolToPlayStopTextConverter.cs` - 재생 상태에 따른 텍스트 변환

### 핵심 기능
- `PlaySound.cs` - System.Media.SoundPlayer를 사용하여 각 채널에 다른 사인파 주파수를 가진 스테레오 WAV 오디오 생성

## 주요 종속성

- **CommunityToolkit.Mvvm (8.4.0)** - 소스 생성기 기반 MVVM 프레임워크, ObservableObject, RelayCommand, Messaging 제공
- **MaterialDesignThemes (5.2.2-ci998)** - Material Design 컴포넌트 및 스타일
- **MaterialDesignColors (5.2.2-ci998)** - Material Design 색상 팔레트

## 네비게이션 아키텍처

앱은 MainViewModel이 제어하는 프레임 기반 네비게이션을 사용합니다. 네비게이션 명령은 메인 윈도우의 네비게이션 바를 유지하면서 세 페이지 간을 전환합니다. 각 페이지는 중앙 프레임 요소에 로드됩니다.

## UI/UX 디자인 가이드라인

### 색상 팔레트
- **배경**: #0E0E1A (짙은 남색)
- **카드 배경**: #1A1A2E (어두운 남색)
- **주요 강조색**: #6C63FF (보라색)
- **텍스트**: 
  - 제목: #FFFFFF (흰색)
  - 본문: #E0E0E0 (밝은 회색)
  - 보조: #B0B0B0 (중간 회색)

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
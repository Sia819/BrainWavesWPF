# BrainWaves - 바이노럴 비트 생성기

BrainWaves는 바이노럴 비트(Binaural Beats)를 생성하는 Windows 데스크톱 애플리케이션입니다. 좌우 귀에 약간 다른 주파수의 소리를 들려주어 뇌파를 특정 상태로 유도할 수 있습니다.

## 주요 기능

### 🎵 바이노럴 비트 생성
- 좌우 독립적인 주파수 설정 (0.01Hz 단위로 정밀 조절)
- 실시간 WAV 오디오 생성 및 재생
- 각 채널별 볼륨(게인) 조절

### 🧠 뇌파 유형별 프리셋
- **13개의 사전 설정된 프리셋**
  - 수면 유도 (Delta파: 0.5-3Hz)
  - 명상 및 이완 (Theta파: 3-8Hz, Alpha파: 8-12Hz)
  - 집중력 향상 (Beta파: 12-38Hz)
  - 인지 기능 향상 (Gamma파: 38-42Hz)

### 📊 주요 프리셋 목록
| 프리셋 | 뇌파 유형 | 주파수 차이 | 용도 |
|--------|-----------|-------------|------|
| Deep Sleep | Delta | 1.63Hz | 깊은 수면 |
| Sleep | Delta | 2Hz | 일반 수면 |
| Meditation | Theta | 4.5Hz | 명상 |
| Relax | Theta | 4.56Hz | 휴식 |
| Creative | Beta | 28.13Hz | 창의성 |
| Focus | Beta | 29Hz | 집중 |
| Cognitive tasks | Gamma | 40Hz | 인지 작업 |

## 시스템 요구사항

- Windows 10 이상
- .NET 8.0 Runtime
- 스테레오 헤드폰 또는 이어폰 (바이노럴 비트 효과를 위해 필수)

## 설치 방법

### 옵션 1: 소스 코드에서 빌드
```bash
# 저장소 클론
git clone https://github.com/yourusername/BrainWavesWPF.git
cd BrainWavesWPF

# 빌드
dotnet build

# 실행
dotnet run --project BrainWaves/BrainWaves/BrainWaves.csproj
```

### 옵션 2: 릴리스 버전 다운로드
1. [Releases](https://github.com/yourusername/BrainWavesWPF/releases) 페이지에서 최신 버전 다운로드
2. ZIP 파일 압축 해제
3. `BrainWaves.exe` 실행

## 사용 방법

1. **Waves 탭**: 좌우 주파수를 직접 조절
   - +/- 버튼으로 0.01Hz 단위 조절
   - 게인 슬라이더로 볼륨 조절

2. **Presets 탭**: 목적에 맞는 프리셋 선택
   - 원하는 프리셋 클릭
   - 자동으로 최적 주파수 설정

3. **재생**: Play 버튼을 눌러 바이노럴 비트 시작

## 주의사항

- 바이노럴 비트는 헤드폰이나 이어폰을 통해서만 효과가 있습니다
- 간질 또는 발작 장애가 있는 경우 사용을 피하세요
- 운전이나 기계 조작 중에는 사용하지 마세요
- 불편함을 느끼면 즉시 사용을 중단하세요

## 기술 스택

- **프레임워크**: WPF (.NET 8.0)
- **아키텍처**: MVVM 패턴
- **주요 라이브러리**:
  - CommunityToolkit.Mvvm (8.4.0)

## 라이선스

이 프로젝트는 [MIT 라이선스](LICENSE)를 따릅니다.

## 기여 방법

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 연락처

프로젝트 관련 문의사항이 있으시면 이슈를 생성해 주세요.
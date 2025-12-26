# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Related Documents

- [MENTOR_RULE.md](./MENTOR_RULE.md) - 멘토 역할 및 가이드라인

## Project Overview

DevSurvivor는 Unity 2D로 제작된 뱀파이어 서바이벌 스타일 게임입니다. 플레이어가 끊임없이 몰려오는 적들을 피하며 생존하는 게임입니다.

## Build & Run

Unity Editor에서 직접 빌드합니다:
- **Play Mode**: Unity Editor에서 Play 버튼 클릭
- **Build**: File > Build Settings > Build

## Architecture

### Core Systems

**GameManager** (싱글톤)
- 전역 접근점 (`GameManager.instance`)
- 게임 시간 관리 (`gameTime`, 최대 20초)
- `PoolManager`와 `Player` 참조 보유

**PoolManager** - 오브젝트 풀링 시스템
- `prefabs[]`: 풀링할 프리팹 배열
- `Get(index)`: 비활성화된 오브젝트 반환 또는 신규 생성
- 프리팹과 리스트가 1:1 매핑

**Spawner** - 적 생성 시스템
- 자식 Transform에서 스폰 포인트 수집
- `SpawnData`: 레벨별 적 속성 (스프라이트, 시간, 체력, 속도)
- 게임 시간 10초마다 레벨 증가

### Game Objects

**Player**
- 키보드 입력 (WASD/화살표)으로 이동
- `Rigidbody2D.MovePosition()` 물리 기반 이동
- 이동 방향에 따른 스프라이트 플립

**Enemy**
- 플레이어 추적 (`target.position - rigid.position`)
- `Init(SpawnData)`: 스폰 시 데이터 초기화
- `animCon[]`: 적 종류별 애니메이터

**Reposition** - 무한 맵 시스템
- `Ground` 태그: 타일맵 40 단위 재배치
- `Enemy` 태그: 플레이어 앞쪽으로 재배치

### Data Flow

```
GameManager.instance
    ├── player (Player)
    └── pool (PoolManager)
            └── Get(index) → Enemy
                    └── Init(SpawnData)
```

## Scripts Location

모든 게임 스크립트: `Assets/Undead Survivor/Scripts/`

## Code Conventions

- Unity C# 스크립팅 표준 사용
- 컴포넌트 참조는 `Awake()`에서 `GetComponent<T>()` 초기화
- 물리 이동은 `FixedUpdate()`에서 처리
- `using System;`과 `using UnityEngine;` 동시 사용 시 `Random` 클래스 모호성 주의 → `UnityEngine.Random` 명시 또는 불필요한 using 제거

# 📌 계산기 프로젝트 기능 명세서
---

## 👥 역할 분담

| 이름     | 담당 역할                                     |
|----------|-----------------------------------------------|
| **공동개발** | UI 구현 |
| **홍아랑** | 고급 연산(루트, 제곱, 파이, 10의 지수수 등) 기능 개발 |
| **이현배** | 기본 연산(사칙연산, 부호, 클리어), 입력 처리 및 이벤트 로직 구현 |

---

##  🔧 설계 흐름

> 💡 계산기 프로그램은 **입력 → 파싱 → 연산 → 출력** 흐름으로 구성됩니다 !!

### 📝 1. 수식 입력 처리
- 사용자 입력을 `TextBox`에서 문자열로 수신  
  예: `"22+3-5×8"`
- `char.IsDigit()`를 활용하여 숫자와 연산자를 판별
- 숫자는 `double 리스트`, 연산자는 `char 리스트`에 저장
- 마지막 숫자는 `!string.IsNullOrEmpty(temp)`로 null 체크 후 리스트에 추가


### 🧮 2. 연산자 파싱 및 처리
- 연산자 리스트(`char[]`)를 순차적으로 순회
- 조건문(`if/else`)으로 각 기호에 해당하는 연산 분기
- 예: `π`, `√`, `^`, `!`, `+`, `-`, `×`, `÷`, `%` 등 처리

<p align="center">
  <img src="https://github.com/user-attachments/assets/63f16d72-56c5-4f26-b3ac-12f59d191d79" width="300" />
</p>

### 🔢 3. 연산 우선순위 적용
1. **고급 연산**: `^`, `√`, `π`, `!`
<p align="center">
  <img src="https://github.com/user-attachments/assets/7587b248-b03c-417f-bce9-e7045fec2b9d" width="300" />
  <img src="https://github.com/user-attachments/assets/95945748-1e76-453a-bc1c-2405828c4558" width="300" />
</p>


2. **중간 연산**: `×`, `%`  
<p align="center">
  <img src="https://github.com/user-attachments/assets/0722d9de-183a-4da1-b7fb-43bf6bd20ea9" width="300" />
</p>

3. **기본 연산**: `+`, `-`  
<p align="center">
  <img src="https://github.com/user-attachments/assets/3446e684-2d5a-4cab-bd20-2e1339f014d3" width="300" />
</p>

4. **부가 연산**: `|x|`, `x²`, `±`
<p align="center">
  <img src="https://github.com/user-attachments/assets/40a8e2d9-7c35-4f42-ba26-aa1c8755074a" width="300" />
</p>



### 🧠 4. 특수 연산 로직
- `e` (지수): `NumberStyles.Float`로 파싱 시 자동 처리
  - ex) 3e2 = 3 * 10^2 = 300으로 자동 변환 !!
 <p align="center">
   <img src="https://github.com/user-attachments/assets/4ea862d6-256c-4fb3-8ce6-e8e0a8609f81" width="40%" />
   <img src="https://github.com/user-attachments/assets/8a826085-3709-472d-896c-c571c431361c" width="40%" />
  </p>

- `π`: 상수로 변환하여 연산에 사용
- `x^y`: 제곱 계산 (x, y 중 하나라도 없을 경우 예외 발생)



### 🧼 5. 예외 처리 및 부가 기능
- `Clear`: 전체 식 제거
- `Backspace`: 마지막 한 문자 제거
- `0으로 나누기`: 무한대(♾️) 처리
- 잘못된 수식: MessageBox로 에러 메시지 출력


---

## 🧮 핵심 기능 정의

### 🔢 기본 연산
- ➕ 더하기  [Add]
- ➖ 빼기  [Sub]
- ✖️ 곱하기  [Mul]
- ➗ 나누기 + 나머지 (a ÷ b = q … r)  [Div]

### 🧠 고급 수학 연산
- √ 루트 (제곱근)  [Square]
- xʸ 제곱 (x^y)  [PowCal]
- x² 제곱 (x^2)  [PowCal]
- 🔟 10의 제곱 (10 × x) 
- π 파이 상수 입력 

### 🧾 수 관련 변환
- 🔁 절대값 (`Math.Abs(x)`)  [AbsCal]
- 🔃 부호 변환 (x → -x) 

### ⏳ 기타 연산
- ❗ 팩토리얼 (n!)  [Pactorial]
- 🔘 소수점 입력 
- `=`: 계산 결과 출력
- 🧼 클리어 (모든 값 초기화)  
- ⬅️ 한 자리씩 지우기 (Backspace)  

---

## 🎨 테마 설정 기능

> 🧩 사용자가 원하는 분위기에 맞게 계산기의 전체 색상을 변경할 수 있는 기능입니다 !

### 🔘 테마 선택 방식
- 계산기 우측에 위치한 **라디오 버튼** 3개 중 하나를 선택하면,
- 아래 항목들의 색상이 즉시 변경됩니다:
  - **버튼(Button) 배경색**
  - **출력 영역(TextBox) 배경색**
  - **Form 전체 배경색(BackColor)**

### 🌈 제공 테마

| 테마 이름   | 색상 구성 예시                         | 설명 |
|------------|----------------------------------------|------|
| **Default** | 흰색 버튼, 검은 글자, 연한 회색 배경     | 클래식하고 가독성 높은 기본 설정 |
| **Beige** | 베이지 배경, 연노랑 톤 버튼 | 따뜻하고 차분한 분위기 |
| **Soda** | 시원한 블루 배경, 연블루 톤 버튼 | 맑고 청량한 시각적 느낌 |

---

### 🛠️ 구현 방식
- `RadioButton.CheckedChanged` 이벤트를 통해 테마 변경 로직 수행
- 각 테마에 맞는 색상 코드는 `ColorTranslater.FromHTML(...)` 또는 `SystemColors` 등으로 지정
- 모든 컨트롤(Button, TextBox 등)은 루프를 통해 일괄 적용


---
##  🏁 최종 완성본 예시

<p align="center">
  <img src="https://github.com/user-attachments/assets/78e9fb98-4d6b-4523-97a4-aa7258d4bbcc" width="32%" />
  <img src="https://github.com/user-attachments/assets/55baebb4-de1a-4123-b1e4-d08b78f2e004" width="32%" />
  <img src="https://github.com/user-attachments/assets/f24066a7-0f51-4205-8e58-d416b8f376fd" width="32%" />
</p>






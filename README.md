# ⏰ HoraCerta  

O **HoraCerta** é uma aplicação desenvolvida em **ASP.NET Core MVC** com **Clean Architecture**, que tem como objetivo facilitar o **agendamento e gerenciamento de horários** de forma simples, organizada e escalável.  

---

## 🚀 Objetivo  

Oferecer uma solução que permita que usuários realizem agendamentos de forma prática, evitando conflitos de horários e garantindo maior controle para administradores e prestadores de serviço.  

---

## 🏗️ Arquitetura  

O projeto segue os princípios da **Clean Architecture**, garantindo separação de responsabilidades, facilidade de manutenção e escalabilidade:  

- **Presentation** → camada responsável pelas interfaces (MVC).  
- **Application** → contém casos de uso, DTOs, serviços e validações.  
- **Domain** → entidades e regras de negócio.  
- **Infrastructure** → persistência de dados, repositórios e integrações.  

---

## ⚙️ Tecnologias Utilizadas  

- **ASP.NET Core 8 MVC**  
- **C#**  
- **Entity Framework Core**  
- **SQL Server** (ou outro banco de dados que você escolher)  
- **Clean Architecture**  

---

## 📌 Funcionalidades (em desenvolvimento)  

- Cadastro de usuários  
- Gerenciamento de agendamentos  
- Validação de conflitos de horário  
- Autenticação e autorização (roles)  
- Painel administrativo para controle de agendas  

---

## 🔮 Futuras Implementações  

- Notificações por e-mail  
- Integração com calendário (Google/Outlook)  
- Relatórios de uso  

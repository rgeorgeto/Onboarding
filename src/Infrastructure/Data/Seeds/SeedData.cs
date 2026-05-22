using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Data.Seeds;

public static class SeedData
{
    public static List<Modulo> ModulosSeed => new()
    {
        new Modulo(
            nome: "Boas-Vindas",
            descricao: "Apresentação do escritório, valores e cultura",
            tipo: TipoModulo.Informativo,
            ordem: 1,
            diasParaLiberar: 0,
            prazoEmDias: null,
            icone: "👋",
            cor: "#4F46E5",
            possuiForm: false,
            formUrl: null
        ),
        new Modulo(
            nome: "Documentos e Contratos",
            descricao: "Assinatura de contrato de trabalho, termos de confidencialidade e cessão de direitos",
            tipo: TipoModulo.Obrigatorio,
            ordem: 2,
            diasParaLiberar: 0,
            prazoEmDias: 5,
            icone: "📄",
            cor: "#059669",
            possuiForm: true,
            formUrl: "/onboarding/documentos"
        ),
        new Modulo(
            nome: "Sistemas e Acessos",
            descricao: "Configuração de e-mail, acesso ao Protheus, Legaldesk e demais sistemas",
            tipo: TipoModulo.Obrigatorio,
            ordem: 3,
            diasParaLiberar: 0,
            prazoEmDias: 7,
            icone: "💻",
            cor: "#2563EB",
            possuiForm: false,
            formUrl: null
        ),
        new Modulo(
            nome: "Políticas e Compliance",
            descricao: "Leitura e ciência do código de conduta, políticas internas e LGPD",
            tipo: TipoModulo.Obrigatorio,
            ordem: 4,
            diasParaLiberar: 0,
            prazoEmDias: 10,
            icone: "📋",
            cor: "#DC2626",
            possuiForm: true,
            formUrl: "/onboarding/politicas"
        ),
        new Modulo(
            nome: "Treinamentos Iniciais",
            descricao: "Capacitação básica sobre ferramentas e processos do escritório",
            tipo: TipoModulo.Obrigatorio,
            ordem: 5,
            diasParaLiberar: 0,
            prazoEmDias: 15,
            icone: "🎓",
            cor: "#7C3AED",
            possuiForm: false,
            formUrl: null
        ),
        new Modulo(
            nome: "Equipe e Integração",
            descricao: "Apresentação aos colegas, mentor designado e cronograma de 1-on-1",
            tipo: TipoModulo.Informativo,
            ordem: 6,
            diasParaLiberar: 0,
            prazoEmDias: null,
            icone: "🤝",
            cor: "#0891B2",
            possuiForm: false,
            formUrl: null
        ),
        new Modulo(
            nome: "Feedback 30 dias",
            descricao: "Avaliação inicial com gestor sobre adaptação e primeiras entregas",
            tipo: TipoModulo.Obrigatorio,
            ordem: 7,
            diasParaLiberar: 30,
            prazoEmDias: 5,
            icone: "📊",
            cor: "#D97706",
            possuiForm: true,
            formUrl: "/onboarding/feedback-30"
        ),
        new Modulo(
            nome: "Metas e Avaliação 90 dias",
            descricao: "Definição e alinhamento de metas do período de experiência",
            tipo: TipoModulo.Obrigatorio,
            ordem: 8,
            diasParaLiberar: 60,
            prazoEmDias: 10,
            icone: "🎯",
            cor: "#65A30D",
            possuiForm: true,
            formUrl: "/onboarding/metas-90"
        ),
    };

    public static List<Conquista> ConquistasSeed => new()
    {
        new Conquista("Primeiros Passos", "Concluiu o primeiro módulo do onboarding", "🥇", 1, "M1"),
        new Conquista("Documentação em Dia", "Finalizou todos os documentos e contratos", "📜", 2, "M1-M4"),
        new Conquista("Conectado", "Completou a configuração de sistemas e acessos", "🔗", 3, "M1-M4"),
        new Conquista("Expert em Compliance", "Concluiu todas as políticas e treinamentos de compliance", "🛡️", 4, "M1-M4"),
        new Conquista("Onboarding Completo", "Finalizou 100% do programa de onboarding", "🏆", 5, "Todos"),
        new Conquista("Meta Atingida", "Cumpriu as metas dos 90 dias", "🚀", 6, "M9"),
    };
}

import { Question } from '../types';

const makeOptions = (
  a: [string, string],
  b: [string, string],
  c: [string, string],
  d: [string, string],
  e: [string, string]
) => [
  { id: 'A' as const, text: a[0], explanation: a[1] },
  { id: 'B' as const, text: b[0], explanation: b[1] },
  { id: 'C' as const, text: c[0], explanation: c[1] },
  { id: 'D' as const, text: d[0], explanation: d[1] },
  { id: 'E' as const, text: e[0], explanation: e[1] }
];

export const questions: Question[] = [
  {
    id: 1, topic: 'Modelo OSI / Camada Física', difficulty: 'fácil',
    statement: 'Em qual camada do modelo OSI ocorre a transmissão de bits pelo meio físico?',
    options: makeOptions(
      ['Camada Física.', 'Correta: é a camada responsável por sinais elétricos/ópticos e transmissão bruta de bits.'],
      ['Camada de Rede.', 'Camada de Rede cuida de endereçamento lógico e roteamento, não da transmissão física de bits.'],
      ['Camada de Enlace.', 'A Enlace enquadra quadros e detecção de erros locais, mas usa a Física para transmitir sinais.'],
      ['Camada de Sessão.', 'Sessão gerencia diálogo entre aplicações, não o meio de transmissão.'],
      ['Camada de Transporte.', 'Transporte trata de confiabilidade e controle fim a fim, não do sinal físico.']
    ),
    correctOptionId: 'A',
    correctExplanation: 'A camada Física transmite bits por sinais no meio de comunicação.',
    conceptSummary: 'Camada Física = sinal e bits no meio.'
  },
  {
    id: 2, topic: 'Cabos blindados', difficulty: 'fácil',
    statement: 'Qual a principal finalidade de cabos com blindagem como FTP, STP e SFTP/SSTP?',
    options: makeOptions(
      ['Aumentar a taxa de transmissão do cabo.', 'Blindagem não define sozinha taxa máxima; depende da categoria e do padrão.'],
      ['Proteger o canal de transmissão contra ruídos externos.', 'Correta: a blindagem reduz interferência eletromagnética.'],
      ['Aumentar a velocidade com que os dados são transmitidos.', 'Velocidade efetiva depende de protocolo e infraestrutura geral, não só blindagem.'],
      ['Diminuir a velocidade com que os dados são transmitidos.', 'Não é finalidade de projeto de cabos blindados.'],
      ['Assegurar que os dados não serão roubados durante a transmissão.', 'Blindagem ajuda contra EMI, não substitui criptografia.']
    ),
    correctOptionId: 'B', correctExplanation: 'Blindagem protege contra ruído e interferência.', conceptSummary: 'Blindagem melhora integridade do sinal.'
  },
  {
    id: 3, topic: 'LAN e WAN', difficulty: 'médio',
    statement: 'Sobre LAN e WAN, quais afirmativas estão corretas? I, III e V.',
    options: makeOptions(
      ['I, III e IV, apenas.', 'IV está incorreta porque LAN tipicamente não cobre grandes distâncias de quilômetros.'],
      ['I, III e V, apenas.', 'Correta: LAN atende ambientes locais, geralmente com alta taxa e uso comum de Ethernet.'],
      ['II, III e V, apenas.', 'II não é característica obrigatória de LAN.'],
      ['II, III, IV e V, apenas.', 'Inclui afirmativas incorretas sobre roteamento obrigatório e alcance.'],
      ['I, II, III, IV e V.', 'Nem todas as afirmativas são verdadeiras.']
    ),
    correctOptionId: 'B', correctExplanation: 'I, III e V são as afirmações corretas.', conceptSummary: 'LAN é rede local, rápida e comum em Ethernet.'
  },
  {
    id: 4, topic: 'Topologia de rede', difficulty: 'médio',
    statement: 'Assinale a opção correta sobre asserções de topologia física e lógica.',
    options: makeOptions(
      ['As asserções I e II são falsas.', 'I é verdadeira.'],
      ['I é falsa e II é verdadeira.', 'I está correta; portanto essa alternativa é inválida.'],
      ['I verdadeira e II falsa.', 'Correta: a definição física de topologia é válida, mas a justificativa da II está incorreta.'],
      ['I e II verdadeiras, mas II não justifica I.', 'II não é verdadeira no enunciado apresentado.'],
      ['I e II verdadeiras e II justifica I.', 'II não justifica porque está incorreta.']
    ),
    correctOptionId: 'C', correctExplanation: 'I é verdadeira e II é falsa.', conceptSummary: 'Topologia pode ser física ou lógica; não são equivalentes em qualquer cenário.'
  },
  {
    id: 5, topic: 'TCP e UDP', difficulty: 'médio',
    statement: 'Sobre diferenças entre TCP e UDP, qual alternativa está correta?',
    options: makeOptions(
      ['I e II, apenas.', 'Correta: TCP é confiável e orientado à conexão; UDP é não orientado e mais leve.'],
      ['I e V, apenas.', 'V é falsa: TCP também usa portas.'],
      ['II e IV, apenas.', 'IV é falsa: UDP não faz confirmação de conexão.'],
      ['I, II e IV, apenas.', 'IV torna a alternativa incorreta.'],
      ['II, III e V, apenas.', 'III e V são falsas.']
    ),
    correctOptionId: 'A', correctExplanation: 'Somente I e II são verdadeiras.', conceptSummary: 'TCP prioriza confiabilidade; UDP prioriza baixa sobrecarga.'
  },
  {
    id: 6, topic: 'Meios de transmissão', difficulty: 'fácil',
    statement: 'Qual item é um meio de transmissão em redes?',
    options: makeOptions(
      ['Um endereço IP.', 'Endereço IP é identificação lógica, não meio físico.'],
      ['Um cabo coaxial.', 'Correta: cabo coaxial é um meio físico de transmissão.'],
      ['Um firewall de rede.', 'Firewall é mecanismo de segurança.'],
      ['Um roteador de banda larga.', 'Roteador é equipamento de interconexão.'],
      ['Um aplicativo de mensagens.', 'Aplicativo é camada de aplicação.']
    ),
    correctOptionId: 'B', correctExplanation: 'Cabo coaxial é meio de transmissão físico.', conceptSummary: 'Meio de transmissão carrega sinais de dados.'
  },
  {
    id: 7, topic: 'TCP/IP', difficulty: 'fácil',
    statement: 'Qual era o objetivo previsto para o TCP/IP?',
    options: makeOptions(
      ['Uma porta dos fundos para redes de guerra.', 'Afirmação sem base técnica e histórica correta.'],
      ['Um endereçamento de redes apenas.', 'TCP/IP vai além de endereçamento: define comunicação por protocolos.'],
      ['Procedimentos para canais físicos conectados apenas.', 'TCP/IP não se limita a um meio físico específico.'],
      ['Uma forma de melhorar telégrafos.', 'Não descreve o propósito real da pilha.'],
      ['Um conjunto de protocolos para comunicação integrada e confiável.', 'Correta: a pilha padroniza comunicação entre hosts e redes.']
    ),
    correctOptionId: 'E', correctExplanation: 'TCP/IP define protocolos para interoperabilidade entre sistemas.', conceptSummary: 'TCP/IP é a base da comunicação na Internet.'
  },
  {
    id: 8, topic: 'Hardware, software e firewall', difficulty: 'médio',
    statement: 'Sobre hardware/software em redes, qual alternativa está correta?',
    options: makeOptions(
      ['Fibra óptica armazena dados.', 'Fibra transmite sinais, não é dispositivo de armazenamento.'],
      ['Roteador é software.', 'Roteador é equipamento (hardware) com firmware/software embarcado.'],
      ['IP é hardware e controla velocidade.', 'IP é endereço lógico e não define velocidade diretamente.'],
      ['Sistema operacional é hardware.', 'Sistema operacional é software.'],
      ['Firewall pode combinar appliance e software para monitorar e controlar tráfego.', 'Correta: firewall pode existir em dispositivos dedicados e em software.']
    ),
    correctOptionId: 'E', correctExplanation: 'Firewall aplica regras de controle de tráfego.', conceptSummary: 'Segurança de rede combina hardware e software.'
  },
  {
    id: 9, topic: 'História da Internet', difficulty: 'fácil',
    statement: 'Qual alternativa descreve corretamente a evolução da Internet?',
    options: makeOptions(
      ['Surgiu por demanda comercial privada nos anos 1960.', 'A origem principal foi pesquisa financiada por defesa e academia.'],
      ['ARPANET conectou laboratórios e universidades nos EUA.', 'Correta: foi um passo essencial para a Internet moderna.'],
      ['Foi inicialmente centralizada em um único ponto.', 'A proposta buscava resiliência, não centralização única.'],
      ['Web sempre foi interativa desde o início.', 'Primeiras experiências eram bem mais simples e estáticas.'],
      ['Ficou restrita até os anos 2000.', 'A popularização pública começou antes disso.']
    ),
    correctOptionId: 'B', correctExplanation: 'ARPANET foi precursor importante da Internet.', conceptSummary: 'Internet evoluiu de projetos acadêmico-militares para uso massivo.'
  },
  {
    id: 10, topic: 'F/UTP e S/UTP', difficulty: 'fácil',
    statement: 'Qual é a principal finalidade dos cabos blindados F/UTP e S/UTP?',
    options: makeOptions(
      ['Aumentar taxa de transmissão.', 'Blindagem não garante aumento de taxa por si só.'],
      ['Proteger contra ruídos externos.', 'Correta: reduz interferência eletromagnética.'],
      ['Aumentar velocidade de transmissão.', 'Velocidade depende de vários fatores além da blindagem.'],
      ['Reduzir carga elétrica necessária.', 'Não é objetivo primário desses cabos.'],
      ['Impedir roubo de dados.', 'Proteção contra roubo depende de segurança lógica/criptográfica.']
    ),
    correctOptionId: 'B', correctExplanation: 'Blindagem melhora imunidade a interferência.', conceptSummary: 'EMI afeta qualidade do sinal.'
  },
  {
    id: 11, topic: 'Camada de Enlace', difficulty: 'médio',
    statement: 'Considerando as afirmativas sobre camada de enlace, marque a correta.',
    options: makeOptions(
      ['I e II, apenas.', 'A III e IV também são atribuídas à camada de enlace no contexto da questão.'],
      ['III e IV, apenas.', 'Correta conforme a formulação proposta na prova-base.'],
      ['I, II e III, apenas.', 'Inclui itens fora da combinação considerada correta na base.'],
      ['I, II e IV, apenas.', 'Não corresponde ao gabarito obrigatório da prova-base.'],
      ['II, III e IV, apenas.', 'Também não corresponde ao gabarito obrigatório da base.']
    ),
    correctOptionId: 'B', correctExplanation: 'Conforme a questão-base, a resposta correta é III e IV.', conceptSummary: 'Enlace trata quadros e mecanismos de erro/fluxo no salto local.'
  },
  {
    id: 12, topic: 'Transmissão de vídeo e UDP', difficulty: 'fácil',
    statement: 'Para streaming de vídeo em tempo real com baixa latência, qual protocolo é mais indicado?',
    options: makeOptions(
      ['Link.', 'Não é protocolo de transporte para esse contexto.'],
      ['UDP.', 'Correta: UDP reduz overhead e latência em aplicações sensíveis ao tempo.'],
      ['Rede.', 'Não especifica protocolo de transporte.'],
      ['Aplicação.', 'Camada de aplicação não substitui o transporte.'],
      ['Apresentação.', 'Camada de apresentação não define entrega de datagramas.']
    ),
    correctOptionId: 'B', correctExplanation: 'UDP é comum quando prioridade é velocidade e tolerância a perdas.', conceptSummary: 'Tempo real frequentemente privilegia latência sobre confiabilidade total.'
  },
  {
    id: 13, topic: 'HUB x SWITCH', difficulty: 'médio',
    statement: 'Sobre substituir HUB por SWITCH para reduzir colisões, qual alternativa é correta?',
    options: makeOptions(
      ['I e II verdadeiras, e II justifica I.', 'II é falsa, pois switch melhora desempenho segmentando tráfego.'],
      ['I e II verdadeiras, mas II não justifica I.', 'II não é verdadeira nesse contexto.'],
      ['I verdadeira e II falsa.', 'Correta: HUB difunde para todos; switch comuta por MAC e reduz colisões.'],
      ['I falsa e II verdadeira.', 'I é verdadeira no cenário clássico de hub.'],
      ['I e II falsas.', 'I não é falsa.']
    ),
    correctOptionId: 'C', correctExplanation: 'Switch melhora eficiência ao encaminhar quadros ao destino correto.', conceptSummary: 'Hub compartilha domínio de colisão; switch segmenta tráfego.'
  },
  {
    id: 14, topic: 'LAN peer-to-peer e cliente/servidor', difficulty: 'fácil',
    statement: 'Qual rede pode operar tanto em peer-to-peer quanto em cliente/servidor?',
    options: makeOptions(
      ['Redes domésticas.', 'Pode ocorrer, mas não é a classificação técnica pedida.'],
      ['LAN — Local Area Network.', 'Correta: LAN pode usar diferentes modelos de organização lógica.'],
      ['PAN.', 'PAN é rede pessoal de curta distância.'],
      ['MAN.', 'MAN é metropolitana, não foco da pergunta.'],
      ['WLAN.', 'WLAN é uma LAN sem fio, mas a resposta esperada na base é LAN.']
    ),
    correctOptionId: 'B', correctExplanation: 'LAN suporta ambos os modelos de compartilhamento e controle.', conceptSummary: 'Modelo lógico independe do meio físico local.'
  },
  {
    id: 15, topic: 'Cabo coaxial', difficulty: 'médio',
    statement: 'Qual afirmação sobre cabo coaxial está correta?',
    options: makeOptions(
      ['10Base2 com 500m e 100 dispositivos.', 'Essas características remetem ao 10Base5, não ao 10Base2.'],
      ['Ainda é o padrão moderno principal de LAN.', 'Hoje par trançado e fibra são predominantes em LAN corporativa.'],
      ['Usava terminadores para evitar reflexão de sinal no barramento.', 'Correta: os terminadores preservavam integridade do sinal.'],
      ['10Base5 era cabo fino mais flexível.', '10Base5 era mais espesso e menos flexível.'],
      ['Permitiria dispositivos ilimitados sem perda.', 'Limitações físicas e elétricas sempre existem.']
    ),
    correctOptionId: 'C', correctExplanation: 'Terminação correta evita reflexões e erros no barramento coaxial.', conceptSummary: 'Topologias antigas exigiam cuidados elétricos específicos.'
  },
  {
    id: 16, topic: 'Máquinas virtuais', difficulty: 'médio',
    statement: 'Sobre virtualização, quais afirmações estão corretas?',
    options: makeOptions(
      ['I e II, apenas.', 'III também é verdadeira.'],
      ['II e III, apenas.', 'I também é verdadeira.'],
      ['I e IV, apenas.', 'IV é falsa no trecho sobre limitação de portabilidade e número fixo.'],
      ['I, II e III, apenas.', 'Correta: essas descrevem corretamente VMs e virtualização.'],
      ['II, III e IV, apenas.', 'IV invalida a alternativa.']
    ),
    correctOptionId: 'D', correctExplanation: 'Virtualização oferece isolamento, flexibilidade e múltiplas VMs por host.', conceptSummary: 'Hypervisor abstrai hardware para sistemas convidados.'
  },
  {
    id: 17, topic: 'WLAN, WMAN, WPAN e WWAN', difficulty: 'médio',
    statement: 'Quais afirmativas estão corretas sobre tipos de redes sem fio?',
    options: makeOptions(
      ['I e II, apenas.', 'IV também é verdadeira no cenário de mobilidade ampla.'],
      ['II e III, apenas.', 'III é incorreta para ampla cobertura em viagens.'],
      ['III e IV, apenas.', 'III continua incorreta pois WPAN é curta distância.'],
      ['I, II e IV, apenas.', 'Correta: WLAN local, WMAN urbana e WWAN móvel de longa cobertura.'],
      ['I, II, III e IV.', 'III torna a alternativa incorreta.']
    ),
    correctOptionId: 'D', correctExplanation: 'WPAN é curta distância; não substitui WWAN para mobilidade ampla.', conceptSummary: 'Cada família sem fio atende alcance e uso diferentes.'
  },
  {
    id: 18, topic: 'TCP e congestionamento', difficulty: 'médio',
    statement: 'No contexto da questão-base, qual ação do TCP ajuda a evitar conflitos/congestionamento?',
    options: makeOptions(
      ['Enviar todos em paralelo.', 'Isso aumenta chance de congestionamento.'],
      ['Enviar sempre serial sem controle adaptativo.', 'TCP usa janelas e controle dinâmico, não regra fixa simplista.'],
      ['Reduzir a velocidade entre pacotes quando necessário.', 'Correta no contexto didático: TCP ajusta taxa de envio sob congestionamento.'],
      ['Parar até confirmação de chegada final de cada pacote.', 'TCP usa ACKs e janela deslizante, não parada total entre todos os segmentos.'],
      ['Apenas numerar datagrama.', 'Numeração ajuda ordenação, mas não resolve sozinha congestionamento.']
    ),
    correctOptionId: 'C', correctExplanation: 'TCP usa mecanismos de controle de congestionamento para modular envio.', conceptSummary: 'Controle de congestionamento evita saturação da rede.'
  },
  {
    id: 19, topic: 'Cliente/Servidor', difficulty: 'fácil',
    statement: 'Qual opção descreve corretamente o modelo Cliente/Servidor em LAN?',
    options: makeOptions(
      ['Todos são cliente e servidor simultaneamente.', 'Isso descreve mais o modelo peer-to-peer.'],
      ['Gestão totalmente individual por estação.', 'Não representa a centralização típica do modelo cliente/servidor.'],
      ['Servidor central permite gestão unificada de autenticação e serviços.', 'Correta: centralização melhora governança e manutenção.'],
      ['Não exige ponto central.', 'No modelo cliente/servidor há serviços centrais.'],
      ['Elimina a necessidade de administração.', 'Administração continua necessária, inclusive de forma mais estruturada.']
    ),
    correctOptionId: 'C', correctExplanation: 'Servidor centraliza políticas, identidade e serviços compartilhados.', conceptSummary: 'Cliente/Servidor prioriza controle e padronização.'
  },
  {
    id: 20, topic: 'Camada de Rede e roteamento', difficulty: 'fácil',
    statement: 'No modelo OSI, qual camada é responsável pelo roteamento?',
    options: makeOptions(
      ['Camada Física.', 'Física trata transmissão de bits.'],
      ['Camada de Rede.', 'Correta: camada 3 decide rotas e encaminhamento lógico.'],
      ['Camada de Sessão.', 'Sessão coordena diálogo entre aplicações.'],
      ['Camada de Transporte.', 'Transporte faz entrega fim a fim, não roteamento entre redes.'],
      ['Camada de Enlace de Dados.', 'Enlace trata comunicação no enlace local.']
    ),
    correctOptionId: 'B', correctExplanation: 'Roteamento é função clássica da camada de Rede.', conceptSummary: 'Camada 3 escolhe caminhos entre sub-redes.'
  },
  // Inéditas
  {
    id: 21, topic: 'DNS', difficulty: 'fácil',
    statement: 'Em uma empresa, usuários acessam sistemas por nome (ex.: intranet.empresa.local). Qual serviço traduz nome em IP?',
    options: makeOptions(
      ['DNS.', 'Correta: DNS resolve nomes de domínio para endereços IP.'],
      ['DHCP.', 'DHCP distribui configuração de rede, não resolve nomes.'],
      ['NAT.', 'NAT traduz endereços entre redes, não nomes.'],
      ['ARP.', 'ARP resolve IP para MAC na rede local.'],
      ['SMTP.', 'SMTP é protocolo de envio de e-mail.']
    ),
    correctOptionId: 'A', correctExplanation: 'DNS faz resolução de nomes.', conceptSummary: 'Nome amigável -> IP para conexão.'
  },
  {
    id: 22, topic: 'DHCP', difficulty: 'fácil',
    statement: 'Qual vantagem principal de usar DHCP em uma LAN corporativa?',
    options: makeOptions(
      ['Criptografar todo tráfego automaticamente.', 'DHCP não cifra tráfego.'],
      ['Atribuir IP, máscara, gateway e DNS automaticamente.', 'Correta: DHCP automatiza parâmetros de rede dos clientes.'],
      ['Substituir switch por software.', 'DHCP não substitui equipamentos de camada 2.'],
      ['Eliminar necessidade de gateway.', 'Gateway ainda é necessário para outras redes.'],
      ['Converter IPv4 em IPv6 sem roteador.', 'Não é função do DHCP.']
    ),
    correctOptionId: 'B', correctExplanation: 'DHCP reduz erros de configuração manual e acelera provisionamento.', conceptSummary: 'Configuração dinâmica de hosts.'
  },
  {
    id: 23, topic: 'NAT', difficulty: 'médio',
    statement: 'Por que NAT é comum em roteadores domésticos?',
    options: makeOptions(
      ['Porque troca DNS por HTTP.', 'NAT não altera o papel de protocolos de aplicação.'],
      ['Porque elimina latência da Internet.', 'NAT não elimina latência e pode até adicionar processamento.'],
      ['Porque permite que vários dispositivos privados compartilhem um IP público.', 'Correta: PAT/NAT viabiliza múltiplos hosts internos saindo por um IP público.'],
      ['Porque converte cabos metálicos em fibra.', 'NAT não atua no meio físico.'],
      ['Porque autentica usuários por biometria.', 'Autenticação não é função nativa do NAT.']
    ),
    correctOptionId: 'C', correctExplanation: 'NAT conserva endereços públicos e separa rede interna da externa.', conceptSummary: 'Tradução de endereços entre domínios privado/público.'
  },
  {
    id: 24, topic: 'Máscara de sub-rede', difficulty: 'médio',
    statement: 'Em IPv4, qual é o papel da máscara de sub-rede?',
    options: makeOptions(
      ['Definir a potência do sinal Wi‑Fi.', 'Máscara não tem relação com potência de rádio.'],
      ['Definir porta TCP padrão.', 'Portas pertencem à camada de transporte.'],
      ['Separar bits de rede e de host no endereço IP.', 'Correta: a máscara determina o prefixo da rede.'],
      ['Resolver nome em endereço IP.', 'Isso é função do DNS.'],
      ['Filtrar pacotes maliciosos.', 'Filtragem é papel de firewall/ACL.']
    ),
    correctOptionId: 'C', correctExplanation: 'Máscara indica qual parte identifica a rede e qual identifica hosts.', conceptSummary: 'Sub-rede organiza endereçamento e roteamento.'
  },
  {
    id: 25, topic: 'Gateway padrão', difficulty: 'fácil',
    statement: 'Quando um host precisa acessar outra rede, para onde ele envia o tráfego por padrão?',
    options: makeOptions(
      ['Para o endereço MAC do DNS.', 'DNS não encaminha tráfego entre redes.'],
      ['Para o switch principal.', 'Switch comuta localmente, não é rota padrão entre redes.'],
      ['Para o servidor DHCP.', 'DHCP apenas fornece configurações.'],
      ['Para o gateway padrão configurado.', 'Correta: gateway é o próximo salto para destinos fora da sub-rede local.'],
      ['Para qualquer host com IP maior.', 'Não existe essa regra de roteamento.']
    ),
    correctOptionId: 'D', correctExplanation: 'Gateway padrão é a rota de saída para redes externas.', conceptSummary: 'Sem gateway, host fala apenas com a rede local.'
  },
  {
    id: 26, topic: 'IP público e privado', difficulty: 'médio',
    statement: 'Qual alternativa descreve corretamente IP público e IP privado?',
    options: makeOptions(
      ['IP público é usado apenas em loopback.', 'Loopback é local ao host, não endereço público.'],
      ['IP privado é roteável diretamente na Internet.', 'Faixas privadas não são roteadas publicamente.'],
      ['IP público só existe em IPv6.', 'IPv4 também possui endereços públicos.'],
      ['IP privado é único no mundo inteiro e público pode repetir.', 'É o contrário na prática de roteamento global.'],
      ['IP público é visível na Internet; IP privado é interno e normalmente sai via NAT.', 'Correta: descreve uso típico dos dois contextos de endereços.']
    ),
    correctOptionId: 'E', correctExplanation: 'Endereços privados funcionam internamente e públicos identificam conexões na Internet.', conceptSummary: 'Escopo de roteabilidade define público x privado.'
  },
  {
    id: 27, topic: 'Roteador x Switch', difficulty: 'médio',
    statement: 'Qual diferença principal entre switch e roteador em redes locais?',
    options: makeOptions(
      ['Switch encaminha quadros por MAC na LAN; roteador interliga redes e roteia por IP.', 'Correta: switch atua sobretudo em camada 2 e roteador em camada 3.'],
      ['Roteador funciona apenas sem fio e switch apenas cabeado.', 'Ambos podem operar em cenários variados, inclusive combinados.'],
      ['Switch sempre faz NAT e roteador nunca faz.', 'NAT é típico de roteadores/firewalls, não de switch L2 comum.'],
      ['Roteador não usa tabela de rotas.', 'Usa sim, é essencial para encaminhamento.'],
      ['Switch substitui completamente DNS e DHCP.', 'São funções distintas.']
    ),
    correctOptionId: 'A', correctExplanation: 'Switch comuta tráfego local; roteador escolhe caminho entre redes.', conceptSummary: 'MAC para domínio local, IP para domínios de rede.'
  },
  {
    id: 28, topic: 'Firewall e filtragem', difficulty: 'médio',
    statement: 'Em segurança de rede, o que significa filtragem de pacotes?',
    options: makeOptions(
      ['Compactar arquivos antes de enviar.', 'Compressão não é filtragem de pacotes.'],
      ['Aplicar regras de permissão/bloqueio com base em IP, porta e protocolo.', 'Correta: firewall decide passar ou bloquear conforme política.'],
      ['Converter IPv4 para fibra óptica.', 'Não há relação entre filtragem e meio físico.'],
      ['Garantir banda mínima por usuário automaticamente.', 'Isso está mais ligado a QoS.'],
      ['Duplicar pacotes para evitar perda.', 'Duplicação não é objetivo da filtragem de segurança.']
    ),
    correctOptionId: 'B', correctExplanation: 'Filtragem compara metadados do pacote com políticas de segurança.', conceptSummary: 'Firewall implementa controle de tráfego e redução de risco.'
  },
  {
    id: 29, topic: 'Fibra, latência e throughput', difficulty: 'difícil',
    statement: 'Qual opção está correta sobre desempenho de rede?',
    options: makeOptions(
      ['Largura de banda é o mesmo que latência.', 'São métricas diferentes: capacidade x atraso.'],
      ['Throughput sempre é igual à largura de banda nominal.', 'Na prática há overhead, perdas e contenção.'],
      ['Fibra óptica tende a suportar alta largura de banda e baixa atenuação em longas distâncias.', 'Correta: por isso é muito usada em backbone e enlaces longos.'],
      ['Latência mede apenas quantidade de erros de bit.', 'Latência mede tempo de ida/volta ou atraso de entrega.'],
      ['Throughput aumenta quando o firewall bloqueia mais portas.', 'Bloqueio não implica aumento automático de vazão.']
    ),
    correctOptionId: 'C', correctExplanation: 'Fibra oferece alta capacidade e bom desempenho para longas distâncias.', conceptSummary: 'Banda, latência e throughput são métricas complementares.'
  },
  {
    id: 30, topic: 'HTTP/HTTPS/FTP/SMTP, Wi‑Fi, MAC e ARP', difficulty: 'difícil',
    statement: 'Assinale a alternativa correta sobre protocolos e identificação na rede.',
    options: makeOptions(
      ['SMTP é usado para navegação web segura.', 'Web segura usa HTTPS, não SMTP.'],
      ['HTTP cifra todo conteúdo por padrão.', 'Quem cifra é HTTPS (HTTP sobre TLS).'],
      ['ARP resolve nome de domínio em IP.', 'Quem resolve nome é DNS.'],
      ['Endereço MAC é lógico e muda a cada roteamento.', 'MAC é identificador de interface na camada de enlace local.'],
      ['HTTPS usa TLS para proteger dados; FTP transfere arquivos; SMTP envia e-mails; ARP mapeia IP para MAC na LAN.', 'Correta: a frase relaciona corretamente cada função.']
    ),
    correctOptionId: 'E', correctExplanation: 'Cada protocolo atua em função específica e ARP opera na resolução local IP->MAC.', conceptSummary: 'Entender função de cada protocolo evita confusões de camada.'
  }
];

import React from 'react';
import { QuizMode } from '../types';

interface Props {
  bestScore: number;
  selectedMode: QuizMode;
  onModeChange: (mode: QuizMode) => void;
  onStart: () => void;
}

export const StartScreen = ({ bestScore, selectedMode, onModeChange, onStart }: Props) => (
  <section className="card start-screen">
    <h1>🧠 Prova Interativa de Redes de Computadores</h1>
    <p>
      Entre no laboratório de rede: responda questões sobre OSI, TCP/IP, switches, roteadores, segurança,
      DNS, DHCP, NAT, Wi‑Fi e muito mais.
    </p>

    <div className="network-legend">
      <span>🖧 Switch</span>
      <span>📡 Roteador</span>
      <span>☁️ Nuvem</span>
      <span>🧰 Firewall</span>
      <span>🖥️ Servidor</span>
    </div>

    <div className="mode-box">
      <h2>Modo da prova</h2>
      <label>
        <input type="radio" checked={selectedMode === 'estudo'} onChange={() => onModeChange('estudo')} />
        Modo Estudo (feedback completo após cada resposta)
      </label>
      <label>
        <input type="radio" checked={selectedMode === 'prova'} onChange={() => onModeChange('prova')} />
        Modo Prova (apenas acerto/erro durante a prova)
      </label>
    </div>

    <p className="best-score">Melhor pontuação registrada: <strong>{bestScore}</strong></p>

    <button type="button" className="main-btn" onClick={onStart}>Iniciar Prova</button>
  </section>
);

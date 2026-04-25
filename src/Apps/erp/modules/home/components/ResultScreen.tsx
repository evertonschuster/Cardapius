import React from 'react';
import { QuizMode } from '../types';

interface Props {
  score: number;
  total: number;
  bestScore: number;
  mode: QuizMode;
  onReview: () => void;
  onRetry: () => void;
}

const getPerformanceMessage = (pct: number) => {
  if (pct < 50) return 'Sua rede ainda está com perda de pacotes.';
  if (pct < 75) return 'Conexão estabelecida, mas ainda há gargalos.';
  if (pct < 90) return 'Rede estável e bom domínio dos conceitos.';
  return 'Administrador de redes em modo turbo.';
};

export const ResultScreen = ({ score, total, bestScore, mode, onReview, onRetry }: Props) => {
  const pct = Number(((score / total) * 100).toFixed(1));

  return (
    <section className="card result-screen">
      <h2>📊 Resultado Final</h2>
      <p>Pacotes validados: <strong>{score}</strong> / {total}</p>
      <p>Percentual de acertos: <strong>{pct}%</strong></p>
      <p className="performance-msg">{getPerformanceMessage(pct)}</p>
      <p>Melhor pontuação histórica: <strong>{bestScore}</strong></p>

      <div className="result-actions">
        <button type="button" className="secondary-btn" onClick={onReview}>
          {mode === 'prova' ? 'Ver explicações completas' : 'Revisar questões'}
        </button>
        <button type="button" className="main-btn" onClick={onRetry}>Refazer prova</button>
      </div>
    </section>
  );
};

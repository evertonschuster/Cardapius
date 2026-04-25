import React from 'react';

interface Props {
  current: number;
  total: number;
}

export const ProgressBar = ({ current, total }: Props) => {
  const value = Math.round((current / total) * 100);

  return (
    <div className="progress-wrap" aria-label="Progresso da prova">
      <div className="progress-meta">
        <span>Pacotes transmitidos: {current}/{total}</span>
        <span>{value}%</span>
      </div>
      <div className="progress-track">
        <div className="progress-value" style={{ width: `${value}%` }} />
      </div>
    </div>
  );
};

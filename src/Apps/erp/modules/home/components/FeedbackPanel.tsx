import React from 'react';
import { Question, OptionId } from '../types';

interface Props {
  question: Question;
  selectedOptionId: OptionId;
  mode: 'estudo' | 'prova';
}

export const FeedbackPanel = ({ question, selectedOptionId, mode }: Props) => {
  const selected = question.options.find((o) => o.id === selectedOptionId);
  const correct = question.options.find((o) => o.id === question.correctOptionId);
  const isCorrect = selectedOptionId === question.correctOptionId;

  return (
    <aside className={`card feedback ${isCorrect ? 'ok' : 'error'}`}>
      <h3>{isCorrect ? '✅ Você acertou!' : '❌ Você errou.'}</h3>
      {mode === 'prova' ? (
        <p>
          {isCorrect
            ? 'Resposta registrada com sucesso. O detalhamento completo será mostrado no final da prova.'
            : `Você marcou ${selectedOptionId} e a correta é ${question.correctOptionId}. O detalhamento completo será mostrado no final da prova.`}
        </p>
      ) : (
        <>
          {!isCorrect && (
            <p>
              Você marcou <strong>{selectedOptionId}</strong> e a alternativa correta é{' '}
              <strong>{question.correctOptionId}</strong>.
            </p>
          )}
          <p><strong>Por que a correta está certa:</strong> {question.correctExplanation}</p>
          <p><strong>Conceito-chave:</strong> {question.conceptSummary}</p>
          {selected && !isCorrect && (
            <p><strong>Sobre sua alternativa:</strong> {selected.explanation}</p>
          )}
          <div className="feedback-list">
            {question.options.map((option) => (
              <p key={option.id}><strong>{option.id})</strong> {option.explanation}</p>
            ))}
          </div>
          {correct && <p><strong>Resumo final:</strong> {correct.text}</p>}
        </>
      )}
    </aside>
  );
};

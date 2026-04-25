export type Difficulty = 'fácil' | 'médio' | 'difícil';
export type OptionId = 'A' | 'B' | 'C' | 'D' | 'E';

export interface QuizOption {
  id: OptionId;
  text: string;
  explanation: string;
}

export interface Question {
  id: number;
  topic: string;
  difficulty: Difficulty;
  statement: string;
  options: QuizOption[];
  correctOptionId: OptionId;
  correctExplanation: string;
  conceptSummary: string;
}

export type QuizMode = 'estudo' | 'prova';

export interface UserAnswer {
  questionId: number;
  selectedOptionId: OptionId;
  isCorrect: boolean;
}

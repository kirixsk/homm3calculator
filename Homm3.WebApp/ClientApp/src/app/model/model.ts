export interface CalculatedMonsterValues {
  monster: any,
  averageMonsterCount: number,
  monsterCountDeviation: number,
  minimalCount: number,
  maximalCount: number
}

export interface ReferenceData {
  monsters: Monster[],
  mapObjects: MapObject[],
  towns: string[],
  guessableObjects: string[],
  mapMonsterStrength: KeyValyePair<string, number>[],
  zoneMonsterStrength: KeyValyePair<string, number>[],
  presets: any[]
}

export interface Monster {
  name: string,
  value: number,
  town: string,
  level: number,
  upgradeLevel: number
}

export interface MapObject {
  name: string,
  town: string,
  value: number,
}

export interface KeyValyePair<T1, T2> {
  key: T1,
  value: T2
}

export interface CalculateRequest {
  townName: string,
  mapMonsterStrengthName: string,
  zoneStrengthName: string,
  totalTownZoneCount: string,
  townZoneCounts: { [key: string]: number },
  monsterName: string,
  objectNames: string[],
  week: number
}


export interface GuessedObjectResult {
  mapObject: any
  differenceFromAverage: number;
}

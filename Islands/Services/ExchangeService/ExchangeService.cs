using Islands.DTOs;
using Islands.Exceptions;
using Islands.Models;
using Islands.Models.Enums;
using Islands.Repositories.ClassifiedAdRepository;
using Islands.Repositories.PlayerInformationRepository;

namespace Islands.Services.ClassifiedAdService
{
    public class ExchangeService : IExchangeService
    {
        private readonly IExchangeRepository exchangeRepository;
        private readonly IPlayerRepository playerRepository;

        public ExchangeService(IExchangeRepository adRepository, IPlayerRepository playerRepository)
        {
            exchangeRepository = adRepository;
            this.playerRepository = playerRepository; 
        }

        public async Task AddAsync(string username, NewAdDto newAd)
        {
            try
            {
                Player advertiser = await playerRepository.GetPlayerByUsernameAsync(username);

                switch (newAd.Item)
                {
                    case Item.Coin:
                        if (advertiser.Coins >= newAd.Amount)
                        {
                            advertiser.Coins -= newAd.Amount;
                        }
                        else
                        {
                            throw new InsufficientItemsException();
                        }

                        break;
                    case Item.Wood:
                        if (advertiser.Woods >= newAd.Amount)
                        {
                            advertiser.Woods -= newAd.Amount;
                        }
                        else
                        {
                            throw new InsufficientItemsException();
                        }

                        break;
                    case Item.Stone:
                        if (advertiser.Stones >= newAd.Amount)
                        {
                            advertiser.Stones -= newAd.Amount;
                        }
                        else
                        {
                            throw new InsufficientItemsException();
                        }

                        break;
                    case Item.Iron:
                        if (advertiser.Irons >= newAd.Amount)
                        {
                            advertiser.Irons -= newAd.Amount;
                        }
                        else
                        {
                            throw new InsufficientItemsException();
                        }

                        break;
                }

                Exchange classifiedAd = new()
                {
                    Item = newAd.Item,
                    Amount = newAd.Amount,
                    ReplacementItem = newAd.ReplacementItem,
                    ReplacementAmount = newAd.ReplacementAmount,
                    PublishDate = DateTime.Now,
                    Player = advertiser
                };

                await exchangeRepository.AddAsync(classifiedAd);
                await playerRepository.UpdatePlayerAsync(advertiser);
            }
            catch (Exception ex)
            {
                if (ex is InsufficientItemsException)
                {
                    throw new InsufficientItemsException("There are not enough items.");
                }

                throw new ServiceException("Failed to create exchange.", ex);
            }
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                Exchange classifiedAd = await exchangeRepository.GetByIdAsync(id);
                await exchangeRepository.RemoveAsync(classifiedAd);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to remove exchange.", ex);
            }
        
        }

        public async Task<List<ExchangeDto>> GetAllAsync()
        {
            try
            {
                return await exchangeRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to get exchanges.", ex);
            }
        }

        public async Task<List<ExchangeDto>> GetAllByUsernameAsync(string username)
        {
            try
            {
                return await exchangeRepository.GetAllByUsernameAsync(username);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to get exchanges.", ex);
            }
        }

        public async Task BuyAsync(string customerUsername, int id)
        {
            try
            {
                Exchange ad = await exchangeRepository.GetByIdAsync(id);
                Player buyer = await playerRepository.GetPlayerByUsernameAsync(customerUsername);
                Player advertiser = await playerRepository.GetPlayerByForAdAsync(id);

                switch (ad.ReplacementItem)
                {
                    case Item.Coin:
                        if (buyer.Coins >= ad.ReplacementAmount)
                        {
                            buyer.Coins -= ad.ReplacementAmount;
                            advertiser.Coins += ad.ReplacementAmount;
                        }
                        else
                        {
                            throw new InsufficientItemsException();
                        }

                        break;
                    case Item.Wood:
                        if (buyer.Woods >= ad.ReplacementAmount)
                        {
                            buyer.Woods -= ad.ReplacementAmount;
                            advertiser.Woods += ad.ReplacementAmount;
                        }
                        else
                        {
                            throw new InsufficientItemsException();
                        }

                        break;
                    case Item.Stone:
                        if (buyer.Stones >= ad.ReplacementAmount)
                        {
                            buyer.Stones -= ad.ReplacementAmount;
                            advertiser.Stones += ad.ReplacementAmount;
                        }
                        else
                        {
                            throw new InsufficientItemsException();
                        }

                        break;
                    case Item.Iron:
                        if (buyer.Irons >= ad.ReplacementAmount)
                        {
                            buyer.Irons -= ad.ReplacementAmount;
                            advertiser.Irons += ad.ReplacementAmount;
                        }
                        else
                        {
                            throw new InsufficientItemsException();
                        }

                        break;
                }

                switch (ad.Item)
                {
                    case Item.Coin:
                        buyer.Coins += ad.Amount;

                        break;
                    case Item.Wood:
                        buyer.Woods += ad.Amount;

                        break;
                    case Item.Stone:
                        buyer.Stones += ad.Amount;

                        break;
                    case Item.Iron:
                        buyer.Irons += ad.Amount;

                        break;
                }

                await playerRepository.UpdatePlayerAsync(buyer);
                await playerRepository.UpdatePlayerAsync(advertiser);
                await exchangeRepository.RemoveAsync(ad);
            }
            catch (Exception ex)
            {
                if (ex is InsufficientItemsException)
                {
                    throw new InsufficientItemsException("There are not enough items.");
                }

                throw new ServiceException("Failed to buy exchange.", ex);
            }
        }
    }
}

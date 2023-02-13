using BLL.DTOs;
using BLL.Exceptions;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories.ClassifiedAdRepository;
using DAL.Repositories.PlayerInformationRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.ClassifiedAdService
{
    public class ClassifiedAdService : IClassifiedAdService
    {
        private readonly IClassifiedAdRepository _classifiedAdRepository;
        private readonly IPlayerRepository _playerRepository;

        public ClassifiedAdService(IClassifiedAdRepository classifiedAdRepository, IPlayerRepository playerRepository)
        {
            _classifiedAdRepository = classifiedAdRepository;
            _playerRepository = playerRepository; 
        }

        public void CreateClassifiedAd(string creatorUsername, CreateClassifiedAdDto createClassifiedAd)
        {
            Player creator = _playerRepository.GetPlayerByUsername(creatorUsername);

            switch (createClassifiedAd.Item)
            {
                case ItemType.Coin:
                    if (creator.Coins >= createClassifiedAd.Amount)
                    {
                        _playerRepository.UpdatePlayerCoins(creator, createClassifiedAd.Amount * -1);
                    }
                    else
                    {
                        throw new NotEnoughItemsException("There are not enough items.");
                    }

                    break;
                case ItemType.Wood:
                    if (creator.Woods >= createClassifiedAd.Amount)
                    {
                        _playerRepository.UpdatePlayerWoods(creator, createClassifiedAd.Amount * -1);
                    }
                    else
                    {
                        throw new NotEnoughItemsException("There are not enough items.");
                    }

                    break;
                case ItemType.Stone:
                    if (creator.Stones >= createClassifiedAd.Amount)
                    {
                        _playerRepository.UpdatePlayerStones(creator, createClassifiedAd.Amount * -1);
                    }
                    else
                    {
                        throw new NotEnoughItemsException("There are not enough items.");
                    }

                    break;
                case ItemType.Iron:
                    if (creator.Irons >= createClassifiedAd.Amount)
                    {
                        _playerRepository.UpdatePlayerIrons(creator, createClassifiedAd.Amount * -1);
                    }
                    else
                    {
                        throw new NotEnoughItemsException("There are not enough items.");
                    }

                    break;
            }

            ClassifiedAd classifiedAd = new ClassifiedAd()
            {
                Item = createClassifiedAd.Item,
                Amount = createClassifiedAd.Amount,
                ReplacementItem = createClassifiedAd.ReplacementItem,
                ReplacementAmount = createClassifiedAd.ReplacementAmount,
                PublishDate = DateTime.Now,
                PlayerInformation = creator
            };

            _classifiedAdRepository.CreateClassifiedAd(classifiedAd);
        }

        public void DeleteClassifiedAd(int id)
        {
            ClassifiedAd classifiedAd = _classifiedAdRepository.GetClassifiedAd(id);
            _classifiedAdRepository.DeleteClassifiedAd(classifiedAd);
        }

        public List<ClassifiedAdDto> GetClassifiedAds()
        {
            return _classifiedAdRepository
                .GetClassifiedAds()
                .Select(c => new ClassifiedAdDto()
                {
                    Id = c.Id,
                    Item = c.Item,
                    Amount = c.Amount,
                    ReplacementItem = c.ReplacementItem,
                    ReplacementAmount = c.ReplacementAmount,
                    PublishDate = c.PublishDate
                })
                .ToList();
        }

        public List<ClassifiedAdDto> GetClassifiedAdsByUser(string username)
        {
            return _classifiedAdRepository
                .GetClassifiedAdsByUsername(username)
                .Select(c => new ClassifiedAdDto()
                {
                    Id = c.Id,
                    Item = c.Item,
                    Amount = c.Amount,
                    ReplacementItem = c.ReplacementItem,
                    ReplacementAmount = c.ReplacementAmount,
                    PublishDate = c.PublishDate
                })
                .ToList();
        }

        public void PurchaseClassifiedAd(string customerUsername, int id)
        {
            ClassifiedAd classifiedAd = _classifiedAdRepository.GetClassifiedAd(id);
            Player customer = _playerRepository.GetPlayerByUsername(customerUsername);
            Player advertiser = classifiedAd.PlayerInformation;

            switch (classifiedAd.ReplacementItem)
            {
                case ItemType.Coin:
                    if (customer.Coins >= classifiedAd.ReplacementAmount)
                    {
                        _playerRepository.UpdatePlayerCoins(customer, classifiedAd.ReplacementAmount * -1);
                        _playerRepository.UpdatePlayerCoins(advertiser, classifiedAd.ReplacementAmount);
                    }
                    else
                    {
                        throw new NotEnoughItemsException("There are not enough items to buy this.");
                    }

                    break;
                case ItemType.Wood:
                    if (customer.Woods >= classifiedAd.ReplacementAmount)
                    {
                        _playerRepository.UpdatePlayerWoods(customer, classifiedAd.ReplacementAmount * -1);
                        _playerRepository.UpdatePlayerWoods(advertiser, classifiedAd.ReplacementAmount);
                    }
                    else
                    {
                        throw new NotEnoughItemsException("There are not enough items to buy this.");
                    }

                    break;
                case ItemType.Stone:
                    if (customer.Woods >= classifiedAd.ReplacementAmount)
                    {
                        _playerRepository.UpdatePlayerStones(customer, classifiedAd.ReplacementAmount * -1);
                        _playerRepository.UpdatePlayerStones(advertiser, classifiedAd.ReplacementAmount);
                    }
                    else
                    {
                        throw new NotEnoughItemsException("There are not enough items to buy this.");
                    }

                    break;
                case ItemType.Iron:
                    if (customer.Irons >= classifiedAd.ReplacementAmount)
                    {
                        _playerRepository.UpdatePlayerIrons(customer, classifiedAd.ReplacementAmount * -1);
                        _playerRepository.UpdatePlayerIrons(advertiser, classifiedAd.ReplacementAmount);
                    }
                    else
                    {
                        throw new NotEnoughItemsException("There are not enough items to buy this.");
                    }

                    break;
            }

            switch (classifiedAd.Item)
            {
                case ItemType.Coin:
                    _playerRepository.UpdatePlayerCoins(customer, classifiedAd.Amount);

                    break;
                case ItemType.Wood:
                    _playerRepository.UpdatePlayerWoods(customer, classifiedAd.Amount);

                    break;
                case ItemType.Stone:
                    _playerRepository.UpdatePlayerStones(customer, classifiedAd.Amount);

                    break;
                case ItemType.Iron:
                    _playerRepository.UpdatePlayerIrons(customer, classifiedAd.Amount);

                    break;
            }

            _classifiedAdRepository.DeleteClassifiedAd(classifiedAd);
        }
    }
}
